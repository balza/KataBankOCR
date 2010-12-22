#fsc.exe --target:library -r /opt/NUnit/bin/net-2.0/nunit.framework.dll *.fs
rm *.dll
fsc.exe --target:library -r /usr/lib/cli/nunit.framework-2.4/nunit.framework.dll *.fs
