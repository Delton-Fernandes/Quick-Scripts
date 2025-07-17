
# Step 1: Azure Blob configuration
import os
import tempfile
from tqdm import tqdm
from azure.storage.blob import BlobServiceClient
from pydrive2.auth import GoogleAuth
from pydrive2.drive import GoogleDrive

# === CONFIGURATION ===
AZURE_CONNECTION_STRING = 'BlobEndpoint=https://deltonsstore.blob.core.windows.net/;QueueEndpoint=https://deltonsstore.queue.core.windows.net/;FileEndpoint=https://deltonsstore.file.core.windows.net/;TableEndpoint=https://deltonsstore.table.core.windows.net/;SharedAccessSignature=sv=2024-11-04&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2025-07-17T17:36:15Z&st=2025-07-17T09:21:15Z&spr=https&sig=XQbupAj%2F8tXRXr4MS9jEw32JJP12dAc1HlhktCiXu5o%3D'
AZURE_CONTAINER_NAME = 'dawson'
LOCAL_DOWNLOAD_DIR = './azure_downloads'  # saves files here

# === AUTH GOOGLE DRIVE ===
gauth = GoogleAuth()
gauth.LocalWebserverAuth()  # First time: interactive browser login
drive = GoogleDrive(gauth)

# === CREATE/GET GOOGLE DRIVE FOLDER ===
def get_or_create_folder(drive, folder_name):
    folder_list = drive.ListFile({
        'q': f"title='{folder_name}' and mimeType='application/vnd.google-apps.folder' and trashed=false"
    }).GetList()
    if folder_list:
        return folder_list[0]['id']
    folder_metadata = {
        'title': folder_name,
        'mimeType': 'application/vnd.google-apps.folder'
    }
    folder = drive.CreateFile(folder_metadata)
    folder.Upload()
    return folder['id']

drive_folder_id = get_or_create_folder(drive, 'AzureBackup')

# === CONNECT TO AZURE STORAGE ===
blob_service_client = BlobServiceClient.from_connection_string(AZURE_CONNECTION_STRING)
container_client = blob_service_client.get_container_client(AZURE_CONTAINER_NAME)

# === ENSURE LOCAL FOLDER EXISTS ===
os.makedirs(LOCAL_DOWNLOAD_DIR, exist_ok=True)

# === DOWNLOAD & UPLOAD BLOBS ===
blobs = list(container_client.list_blobs())
print(f"Found {len(blobs)} blobs. Starting transfer...\n")

for blob in tqdm(blobs, desc="Processing Blobs"):
    blob_name = blob.name
    local_file_path = os.path.join(LOCAL_DOWNLOAD_DIR, os.path.basename(blob_name))

    # Download from Azure Blob
    with open(local_file_path, "wb") as file:
        blob_data = container_client.download_blob(blob)
        file.write(blob_data.readall())

    # Upload to Google Drive folder
    gfile = drive.CreateFile({
        'title': os.path.basename(blob_name),
        'parents': [{'id': drive_folder_id}]
    })
    gfile.SetContentFile(local_file_path)
    gfile.Upload()

print("\n✅ All files downloaded and uploaded to Google Drive in folder 'AzureBackup'.")
