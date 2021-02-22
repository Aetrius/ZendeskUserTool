# Zendesk User Tool

This utility allows for users to be exported and users to be deleted in a single format or in bulk.


***
## Getting Setup

### Updating AppConfig
ZendeskUserRemoval.dll.config
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="filePath" value="C:\Export\"/>
    <add key="password" value=""/>
    <add key="userName" value="some_email_here@domain.com"/>
    <add key="baseUrl" value="https://subdomain.zendesk.com"/>
  </appSettings>
</configuration>


Update the password
Update the username
Update the baseUrl

Once this is completed you can modify the file path if needed for the export.

##Disabling Users
Create a text file in the file path folder called disable.txt
i.e. C:\Export\disable.txt

Add the IDs of the zendesk user account to delete.
5590112547,5590112557,5593694908,5640624927
