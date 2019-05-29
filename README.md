# Description
Fast HTTP Server

## Installation
1. Compile copy of webserver
2. Remove `.example` from configuration files
3. Fill `DirectoryRoot` in `server.xml` config
4. Provide `www` directory with websites
5. Fill `vhosts.xml`, as shown in samples below
6. Launch webserver using windows services

### VHosts layout config
Default virtual host is used for routing all requests with some another domain names.

```
<?xml version="1.0" encoding="utf-16"?>
<VirtualHostsConfiguration>
  <DefaultVirtualHost>
    <ServerName>127.0.0.1</ServerName>
    <ServerAlias>localhost</ServerAlias>
    <Directory>localhost</Directory>
    <ErrorLog>localhost.error.log</ErrorLog>
    <AccessLog>localhost.custom.log</AccessLog>
      <DefaultIndex>
        <IndexFile>
          <FileName>index.html</FileName>
        </IndexFile>
      </DefaultIndex>
  </DefaultVirtualHost>
  <VirtualHosts>
  // your configs go here
  </VirtualHosts>
</VirtualHostsConfiguration>
```

### Config with digest auth

```
<VirtualHost>
  <ServerName>s4.localhost</ServerName>
  <ServerAlias>www.s4.localhost</ServerAlias>
  <Directory>s4.localhost</Directory>
  <ErrorLog>s4.localhost.error.log</ErrorLog>
  <AccessLog>s4.localhost.access.log</AccessLog>
  <DefaultIndex>
    <IndexFile>
      <FileName>index.html</FileName>
    </IndexFile>
    <IndexFile>
      <FileName>index.php</FileName>
    </IndexFile>
  </DefaultIndex>
  <DigestAuthentication>
    <DigestAuthConfig>
      <Directory></Directory>
      <Realm>Secure logon</Realm>
      <UserName>admin</UserName>
      <Password>12345</Password>
    </DigestAuthConfig>
  </DigestAuthentication>
</VirtualHost>
```

### Config with basic auth

```
<VirtualHost>
  <ServerName>s6.localhost</ServerName>
  <ServerAlias>www.s6.localhost</ServerAlias>
  <Directory>s6.localhost</Directory>
  <ErrorLog>s6.localhost.error.log</ErrorLog>
  <AccessLog>s6.localhost.access.log</AccessLog>
  <DefaultIndex>
    <IndexFile>
      <FileName>index.htm</FileName>
    </IndexFile>
  </DefaultIndex>
  <BasicAuthentication>
  <BasicAuthConfig>
    <Directory></Directory>
    <Realm>Secure Directory Access</Realm>
    <UserName>admin</UserName>
    <Password>12345</Password>
  </BasicAuthConfig>
  </BasicAuthentication>
</VirtualHost>
```

### Config with several index files

```
<VirtualHost>
  <ServerName>mylocal.host</ServerName>
  <ServerAlias>www.mylocal.host</ServerAlias>
  <Directory>mylocal.host</Directory>
  <ErrorLog>mylocal.host.error.log</ErrorLog>
  <AccessLog>mylocal.host.custom.log</AccessLog>
  <DefaultIndex>
    <IndexFile>
      <FileName>index.html</FileName>
    </IndexFile>
    <IndexFile>
      <FileName>index.php</FileName>
    </IndexFile>
  </DefaultIndex>
</VirtualHost>
```
