import os

# List of folder names
folder_names = [
    "Balance Sheet", "Category", "Chart Of Accounts", "Contracts", "Cost Centre Approvers", 
    "Deals", "Dimension", "Level", "Profit And Loss", "Purchase Order", 
    "Reference Data", "Suppliers", "Tag Report"
]

# Base directory where folders will be created
base_dir = "./"  # Change this to your desired directory

for index, folder_name in enumerate(folder_names):
    # Create a directory named after the index
    folder_path = os.path.join(base_dir, folder_name)
    os.makedirs(folder_path, exist_ok=True)
    
    # Create the first JS file with the name of the folder_name.js
    js_file_path = os.path.join(folder_path, f"{folder_name.replace(' ', '')}.js")
    with open(js_file_path, 'w') as f:
        f.write("// JavaScript file")
    
    # Create the second JS file with the name of folder_nameConfig.js
    config_file_path = os.path.join(folder_path, f"{folder_name.replace(' ', '')}Config.js")
    with open(config_file_path, 'w') as f:
        f.write("// JavaScript config file")

print("Folders and files created successfully.")
