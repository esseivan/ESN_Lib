# ESN_Libraries
Library with multiple tools
Example of use in the Examples folder
## C# Library : EsseivaN_Lib
### Update Checker
Check updates from a file on the publish wesite
Template file can be found in EsseivaN_Lib folder

### WaterMark Textbox/RichTextbox
Add text when textbox is empty (e.g. "Type here...")

### WaterMark
Add the base watermark effect. Require OnFocus_enter, OnFocus_lost function and getText and setText function to be defined.
It can then be applied to any kind of text control that can be focused

### Dialog
Dialog with/out input. Title, message, buttons count, buttons text can be changed.

### Some more quite useless stuff

## SettingsManager
Allow the store of array list in a file, then retrieve it

## Manual Update Checker : ManualUpdateChecker
Similar to the update checker in c# library but it doesn't require to be run by the app itself
It needs an additionnal file named "version.txt" contaning only the version (e.g. "1.2.0"), no line end

## Content replacer : ContentReplacer
Replace text in specified file by either the content of another file or the specified text

## WebEditor : WebEditor
Replace variables in specified file by either the same variable found in another file or the specified text
Template config can be found in WebEditor folder

## SFTP Upload : SFTP_Upload
Tool to easily upload folder content to a sftp target

## PostBuild manager : PostBuild_manager
Postbuild to do the work once the project is built
It create a zip of the output, edit the files for the publish page, modify the version file for the updateChecker

For any issue or features to be implemented, you can contact me or report in the issue tab

# Changelog
## 1.1.0 - ?
### Changes
Bug fixes
More bug fixes
Some features added

## 1.0.0 - Initial public release

Esseiva Nicolas
nicolas.esseiva@gmail.com
