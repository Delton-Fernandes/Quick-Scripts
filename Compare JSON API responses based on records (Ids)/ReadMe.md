
## Usage

Wrote to check the difference between records produced by the Access Dimensions API, when using different parameters.

Checks the JSON API responses in two files in current dir "res1.json" & "res2.json" and return a object containing, 
1. Common ids 
2. Records (ids) only in file 1
3. Records (ids) only in file 2



## API Response JSON Format

{
    Metadata {...}
    Data: {
        Records[
            {
                "unique_id":""
                ...
            }
        ]
    }
}

## 