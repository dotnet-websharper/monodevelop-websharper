MDTOOL=./tools/mdtool
XBUILD=xbuild
NS=MonoDevelop
N=WebSharper
VER=2.5.3
NAME=$(NS).$(N)
PKG=repository/$(NAME)_$(VER).mpack
CONF=Release
DLL=MonoDevelop.WebSharper/bin/$(CONF)/$(NAME).dll

.PHONY: main restore clean install uninstall release

main: $(PKG)

$(PKG): $(DLL)
	$(MDTOOL) setup pack $(DLL)
	mv *.mpack repository/

$(DLL): $(NAME) restore
	$(XBUILD) /p:Configuration=$(CONF)

install: $(PKG)
	$(MDTOOL) setup install -y $(PKG)

uninstall:
	$(MDTOOL) setup uninstall -y $(NAME)

clean:
	$(XBUILD) /p:Configuration=$(CONF) /target:Clean
	rm -rf $(PKG)

packages:
	mono tools/NuGet.exe install WebSharper -o packages -excludeVersion -prerelease
	mono tools/NuGet.exe install FsNuget -o packages -excludeVersion
	mono tools/NuGet.exe install WebSharper.Templates -o packages -excludeVersion -prerelease
	mono tools/NuGet.exe install sharpcompress -o packages -excludeVersion

restore: packages

release: $(PKG)
	$(MDTOOL) setup rep-build repository/
