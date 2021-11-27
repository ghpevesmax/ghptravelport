# GhpTravelport Services.

GhpTravelport is a collection of services written using the .NET framework. *FileWatcher* and *RestWatcher* are the services used to watch, consume and save **.MIR** files for later integration in the *MiAgencia* app.

## Before installing.

GhpTravelport services are packaged as a **.zip** file which contains two folders inside, containing the *FileWatcher* and *RestWatcher* services.

Before continuing to installation, you must create a folder named: `ghptravelport` in Windows root path `C:\`, so your recently created folder would be as follows: `C:\ghptravelport`.

Inside the new folder, create a folder named `dist`, this is going to be the path destination for the unzipped folders, please unzip all the contents and place the service folders (**FileWatcher** and **RestWatcher**). 

- **Avoid** moving/deleting files or altering the folder structure to prevent misbehavior or installation issues

## Installation

Once the zipped files have placed in `C:\ghptravelport\dist`, open a **CMD** as **Administrator** in order to install the service via command-line
- A **command-line per service** is required

Open a command prompt (**as Administrator**) in the path `C:\ghptravelport\dist\FileWatcher` and write the following command:
- `FileWatcherService.exe install start`


Open a command prompt (**as Administrator**) in the path `C:\ghptravelport\dist\RestWatcher` and write the following command:
- `FileToRestService.exe.exe install start`

Once the following commands run both services have been installed and ready to consume .MIR files and sending to the *MiAgencia* app.

### Uninstall
Open a command prompt (**as Administrator**) in the path `C:\ghptravelport\dist\FileWatcher` and write the following command:
- `FileWatcherService.exe uninstall`


Open a command prompt (**as Administrator**) in the path `C:\ghptravelport\dist\RestWatcher` and write the following command:
- `FileToRestService.exe.exe uninstall`



__Important__: `dist` folder should not be removed in order to still using `install` & `uninstall` commands.

## Usage

GhpTravelport services will create the required working directories to succesfully process .MIR files. To let the services process your files, make sure you move every .MIR file into the `C:\ghptravelport` folder.

Once your .MIR files are present in `C:\ghptravelport`, **FileWatcher** service will move them into `C:\ghptravelport\stage` to set them ready to be processed, this way the service ensures .MIR files are ready to be processed and any other program would try to consume them.

All the files in the `C:\ghptravelport\stage` folder are going to be processed, one after the other, the data extracted will be sent to the API which will save the records for later processing.

- Services doesn't have any UI to interact with, so they **run automatically every minute**.