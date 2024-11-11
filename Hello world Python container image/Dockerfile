# Step 1: Use an official Python runtime as the base image
FROM python:3.9-slim

# Step 2: Set the working directory inside the container
WORKDIR /usr/src/app

# Step 3: Copy the requirements.txt file (if you have dependencies)
COPY requirements.txt ./

# Step 4: Install the Python dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Step 5: Copy the rest of your application code
COPY . .

# Step 6: Define the command to run your Python script
CMD [ "python", "./app.py" ]
