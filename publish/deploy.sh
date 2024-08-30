#!/bin/bash

# Define the path to the published folder
PUBLISHED_FOLDER="./publish"

# Check if the folder exists
if [ -d "$PUBLISHED_FOLDER" ]; then
    echo "Running the application locally..."
    dotnet "$PUBLISHED_FOLDER/HR.LeaveManagmant.dll"
else
    echo "Published folder does not exist. Please ensure the build process completed successfully."
fi