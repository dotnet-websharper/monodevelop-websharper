MDTOOL=mdtool
XBUILD=xbuild
NS=MonoDevelop
N=WebSharper
VER=2.5.0
NAME=$(NS).$(N)
PKG=$(NAME)_$(VER).mpack
CONF=Release
DLL=MonoDevelop.WebSharper/bin/$(CONF)/$(NAME).dll

.PHONY: main restore clean install uninstall

main: $(PKG)

$(PKG): $(DLL)
	$(MDTOOL) setup pack $(DLL)

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
	mono tools/NuGet.exe install WebSharper -o packages -excludeVersion

restore: packages
