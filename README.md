# BCSH1 - Téma semestrální práce
Aplikace, která bude sloužit k ověření spotřeby elektrické energie v průběhu roku mezi vyúčtováními. Každý rok je tak trochu překvapení, kolik která domácnost bude doplácet za elektrickou energii. S pomocí této aplikace si uživatel bude moct ověřit, jak na tom je v každém dni v roce.
### Aplikace bude:
	-	Sledovat entity: měření, poplatek, cena
	-	Načítat data od uživatele, který provede měření na elektroměru.
	-	Vycházet z ročního rozpočtu (12 násobek měsíčních záloh) 
	-	Odečítat bude všechny povinné poplatky, ty fixní i ty procentuální
	-	Umožňovat změnu ceny v průběhu roku 
	-	Vykreslovat graf letošní spotřeby 
	-	Porovnávat se spotřebou v minulém roce 
	-	Upozorňovat na příliš vysokou spotřebu 
	-	Ukazovat doporučenou maximální denní spotřebu 
	-	Spravovat více domácností nezávisle na sobě (nebo např. jen místnosti opatřené měřiči spotřeby) 
	-	Ukládat a načítat nastavení a data

## Report:
Rozpracovaná verze. V budoucnu bude aplikace splňovat výše uvedené funkce. V nejbližší době chci aplikaci opatřit testy, pro efektivní kontorlu funkčnosti validace. V tuto chvíli je pro mě výhodnější pracovat s konzolovou verzí. Až budou výpočty správně fungovat, bude vstup i výstup z aplikace skrze grafické prostředí.

### Zatím je implementováno:
	- Struktura zpracování dat v aplikaci
	- Validace vkládaných hodnot a případné vystavení výjimek
	- Všechny potřebné třídy
	- Aplikace je zatím v konzolové verzi s UI. 
### Chyby, kterých si jsem vědom:
	- Kontorla překrývajících se intervalů cen v některých případech nefunguje.
	- Cena musí být číslo s desetinnou čárkou.
