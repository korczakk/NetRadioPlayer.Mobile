1. Appsettings
Aby umożliwić przechowywanie konfiguracji w pliku Appsettings.json należy w projekcie NetRadioPlayer.Mobile utworzyć plik 
Appsettings.production.json i przekopiować do niego strukturę z pliku appsettings.json. Następnie należy wprowadzić odpowiednie 
wartości na podstawie danych z Azure.
W trakcie budowania projektów wykonywane są instrukcje przed budowaniem, które kopiują plik NetRadioPlayer.Mobile\Appsettings.produkction.json 
do folderu Bin (w przypadku aplikacji UWP) lub folderu Assets (w przypadku aplikacji Android) i zamieniają jego nazwę na
Appsettings.json. Ważne jest, aby do folderu Assets w projekcie NetRAdioPlayer.Android skopiować najpierw plik Appsettings.json
i ustawi dla niego Build action na AndroidAsset.