import json

# Load the JSON data from the files
def load_json(file_path):
    with open(file_path, 'r') as file:
        return json.load(file)

# Extract unique IDs from the records in the JSON data
def extract_unique_ids(json_data):
    return {record['uniqueId'] for record in json_data['data']['records']}

# Compare the unique IDs between two sets
def compare_unique_ids(file1, file2):
    # Load the JSON data from each file
    json1 = load_json(file1)
    json2 = load_json(file2)

    # Extract unique IDs
    ids1 = extract_unique_ids(json1)
    ids2 = extract_unique_ids(json2)

    # Find common IDs between the two sets
    common_ids = ids1 & ids2

    # Find IDs present in one set but not the other
    ids_only_in_file1 = ids1 - ids2
    ids_only_in_file2 = ids2 - ids1

    return {
        'common_ids': common_ids,
        'ids_only_in_file1': ids_only_in_file1,
        'ids_only_in_file2': ids_only_in_file2,
    }

# Example usage
file1 = 'res1.json'
file2 = 'res2.json'

result = compare_unique_ids(file1, file2)

print("Common Unique IDs:", result['common_ids'])
print("Unique IDs only in File 1:", result['ids_only_in_file1'])
print("Unique IDs only in File 2:", result['ids_only_in_file2'])
