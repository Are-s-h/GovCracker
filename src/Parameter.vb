Module Parameter

    Public toolTip1 As New ToolTip()
    Public Sub Start()

        '### Güligkeit prüfen. Ablaufdatum 31.12.2023
        Dim DatumG As String = "2023"

        Select Case True
            Case Format(Now, "yyyy") > DatumG
                If My.Settings.ENG = False Then MsgBox("Der Unterstützungszeitraum für diese GovTools-Version ist abgelaufen. Bitte laden Sie eine neue Version unter www.GovCrack.com herunter.")
                If My.Settings.ENG = True Then MsgBox("The support period for this GovTools-Version expired. Please download a new version from www.GovCrack.com.")

                Application.Exit()

                Exit Sub

        End Select
    End Sub

    Public Sub StartPara()

        '### Tooltips laden
        toolTip1.AutoPopDelay = 10000
        toolTip1.InitialDelay = 500
        toolTip1.ShowAlways = True
        toolTip1.SetToolTip(GovTools.btnPrinceCalc, "Rough number of passwords and file size")
        toolTip1.SetToolTip(GovTools.LinkEx1, "Here you can extract hahes online. At your own risk!")
        toolTip1.SetToolTip(GovTools.tbPrince, "Here you can enter e.g. information about the target person.")
        toolTip1.SetToolTip(GovTools.tbCewlSpider, "With 0 only the top page is scanned.")
        toolTip1.SetToolTip(GovTools.cbWordScanner, "Line number output with?")
        toolTip1.SetToolTip(GovTools.pbTrash, "Clear all fields!")
        toolTip1.SetToolTip(GovTools.pbGovC, "Start GovCracker")
        toolTip1.SetToolTip(GovTools.pbENG, "Select English language")
        toolTip1.SetToolTip(GovTools.pbGER, "Select German language")
        toolTip1.SetToolTip(GovTools.pbAbout, "About GovTools")

        '### Auf Deutsch umstellen
        If My.Settings.GER = True Then

            '### Tooltips laden
            toolTip1.AutoPopDelay = 10000
            toolTip1.InitialDelay = 500
            toolTip1.ShowAlways = True
            toolTip1.SetToolTip(GovTools.btnPrinceCalc, "ca. Anzahl der Passwörter und Dateigröße")
            toolTip1.SetToolTip(GovTools.LinkEx1, "Hier können Sie online Hashes extrahieren. Auf eigene Gefahr!")
            toolTip1.SetToolTip(GovTools.tbPrince, "Hier können Sie bspw. Infos zur Zielperson erfassen.")
            toolTip1.SetToolTip(GovTools.tbCewlSpider, "Mit ""0"" wird nur die oberste Seite gescannt.")
            toolTip1.SetToolTip(GovTools.cbWordScanner, "Soll die Trefferzeile ausgegegebn werden?")
            toolTip1.SetToolTip(GovTools.pbTrash, "Alle Felder löschen")
            toolTip1.SetToolTip(GovTools.pbGovC, "Start GovCracker")
            toolTip1.SetToolTip(GovTools.pbENG, "Englisch auswählen")
            toolTip1.SetToolTip(GovTools.pbGER, "Deutsch auswählen")
            toolTip1.SetToolTip(GovTools.pbAbout, "Über GovTools")

            '### Extractor
            GovTools.btnExFile.Text = "Datei auswählen"
            GovTools.lbEx1.Text = "Datei-Format:"

            '### Maskprozessor
            GovTools.lbMask1.Text = "Kommandos:"
            GovTools.lbMask2.Text = "Ausgabe-Typ:"
            GovTools.lbMask3.Text = "Ausgabe in:"
            GovTools.lbMask8.Text = "Wortlänge:"
            GovTools.lbMask9.Text = "bis"
            GovTools.lbMask11.Text = "bis"
            GovTools.lbMask10.Text = "Start / Stop - Position:"
            GovTools.lbMask12.Text = "Max. mehrere Zeichen:"
            GovTools.lbMask14.Text = "Max. vorkomm. Zeichen:"
            GovTools.lbMask13.Text = "(max. Anzahl aufeinanderfolgender Zeichen / min=2)"
            GovTools.lbMask15.Text = "(max. Anzahl des Vorkommens eines Zeichens / min=2)"
            GovTools.cbMask3.Text = "Ausgabedatei"
            GovTools.cbMask4.Text = "Anzahl der Kombinationen berechnen"
            GovTools.lbMask16.Text = "(Wörter o. Zahlen)"

            '### PRINCE
            GovTools.labPrince.Text = "Der Princeprocessor ist ein Passwortkandidaten-Generator und kann als fortgeschrittener Kombinator-Angriff betrachtet" & vbNewLine & "werden. Anstatt zwei verschiedene Wortlisten als Eingabe zu nehmen und dann alle möglichen Wortkombinationen" & vbNewLine & "auszugeben, benutzt Prince nur eine Eingabewortliste und bildet ""Ketten"" aus kombinierten Wörtern."
            GovTools.CheckBoxPrince.Text = "Anfangsbuchstaben zusätzlich groß"
            GovTools.GroupBoxPrince2.Text = "Keyspace berechnen"
            GovTools.btnPrinceCalc.Text = "Keyspace kalkulieren"
            GovTools.btnPrinceFile.Text = "Datei laden"
            GovTools.btnPrinceDefault.Text = "Standard"
            GovTools.lbPrincePass.Text = "Passwörter"
            GovTools.lbPrince1.Text = "Anzahl Permutationen"
            GovTools.lbprince2.Text = "Passwort-Länge"

            '### Wordlister
            GovTools.lbWordTarget.Text = "Zielperson"
            GovTools.lbWordWife.Text = "Ehepartner"
            GovTools.lbWordChild1.Text = "Kind 1"
            GovTools.lbWordChild2.Text = "Kind 2"
            GovTools.lbWordChild3.Text = "Kind 3"
            GovTools.lbWordMother.Text = "Mutter"
            GovTools.lbWordFather.Text = "Vater"
            GovTools.lbWordNickname.Text = "Spitzname"
            GovTools.lbWordName.Text = "Vorname"
            GovTools.lbWordSurname.Text = "Nachname"
            GovTools.lbWordBirthday.Text = "Geburtstag (TTMM)"
            GovTools.lbWordBirthyear.Text = "Geburtsjahr (JJJJ)"
            GovTools.lbWordInfos.Text = "Sonstiges (bspw. Name des Haustieres, Fussballclub, ...). In jedes Feld nur einen Eintrag."
            GovTools.lbWordperm.Text = "Permutationen"
            GovTools.lbWordmin.Text = "min. Länge"
            GovTools.lbWordmax.Text = "max. Länge"
            GovTools.lbWordleet.Text = "Leet-Sprache"
            GovTools.lbWordfirst.Text = "Vorne Großbuchstaben"
            GovTools.lbWordall.Text = "Alles Großbuchstaben"
            GovTools.lbWord1.Text = "Alle Eingaben in Kleinbuchstaben vornehmen! Geburtsdatum ohne Punkte eingeben!"
            GovTools.btnWordOpen.Text = "Datei öffnen"
            GovTools.GroupBoxWord1.Text = "Informationen zur Zielperson"
            GovTools.GroupBoxWord2.Text = "Weitere Informationen zur Zielperson"

            '### CUPP
            GovTools.lbCupp1.Text = "CUPP ist ein Wordlister, der große Wordlists aus subjektiven Informationen" & vbNewLine & "zur Zielperson erstellt."


            '### Cewl
            GovTools.GroupBoxCewl1.Text = "Linux"
            GovTools.lbCewlInstall.Text = "Nun können Sie CeWL installieren:"
            GovTools.GroupBoxcewl3.Text = "so. Einstellungen (optinal)"
            GovTools.lbcewl2.Text = "CeWL kann aus einer URL eine Wordlist und Emailadressen extrahieren. Bitte beachten Sie die" & vbNewLine & "Schreibweise: http:// oder https://. Die Extraktion wird im Ordner ""#_Wordlists"" abgelegt."
            GovTools.lbcewl3.Text = "Ziel-Homepage:"
            GovTools.lbCewl1.Text = "Wenn Sie CeWL erstmalig verwenden, müssen Sie Linux" & vbNewLine & "und die entsprechenden Packages installieren. Drücken" & vbNewLine &
                                  "Sie den Button um Linux zu installieren:"
            GovTools.lbcewl4.Text = "Tiefe (Voreinstellung: 2):"
            GovTools.lbcewl5.Text = "Minimum Wortlänge: "

            '### Combinator
            GovTools.lbComb2.Text = "Datei 1:"
            GovTools.lbComb3.Text = "Datei 2:"
            GovTools.lbComb4.Text = "Datei 3:"
            GovTools.lbComb1.Text = "Combinator kann bis zu drei Wordlists miteinander verbinden. Jedes Wort der zweiten" & vbNewLine & "und dritten Wordlist wird an jedes Wort der ersten Wordlist angehangen. Dies ist vergleichbar" & vbNewLine & "mit der Combinator-Attacke (-a 1)."

            '### Len
            GovTools.lbLen1.Text = "Mit ""Len"" können Sie Passwortkandidaten mit einer bestimmten Länge aus Wordlists" & vbNewLine &
                                   "in eine neue Wordlist extrahieren."
            GovTools.lbLen3.Text = "Wortlänge"
            GovTools.lbLen4.Text = "bis"

            '### Dup
            GovTools.lbDup1.Text = "DupCleaner entfernt aus selbsterstellten Wordlists die Duplikate."
            GovTools.lbDup2.Text = "Wordlist öffnen"
            GovTools.lbDup3.Text = "Zielordner"

            '### Wordlisttools
            GovTools.lbWordT1.Text = "Personen benutzen häufig die selbe Passwortstruktur." & vbNewLine & "Mit diesem Tool können Sie Wordlists in Maskenlisten" & vbNewLine & "umwandeln (.hcmask)"
            GovTools.lbWordT2.Text = "Der Wordlist-Analyser zählt alle Zeichen in einer" & vbNewLine & "Wordlist. Dadurch können häufig verwendete Zeichen" & vbNewLine & "der Zielperson festgestellt werden."
            GovTools.lbWordT3.Text = "Der Wordlist Scanner durchsucht eine Wordlist nach einem" & vbNewLine & "Begriff und gibt die entspr. Zeilennummer aus. Die Datei" & vbNewLine & "finden Sie im Ordner ""#_Wordlists"""

            '### Hash
            GovTools.lblHashGen.Text = "Hier können Sie zu Testzwecken einen MD5-Hash erstellen. Der erstellte Hash" & vbNewLine & " wird im Ordner ""#_Hashout"" abgespeichert. Für besondere Encodings" & vbNewLine & " (bspw. ISO-8859-1) können Sie folgenden Link benutzen:"

            '###Bulk
            GovTools.lbBulk1.Text = "Image-Datei:"
            GovTools.lbBulk2.Text = "Wortlänge:"
            GovTools.lbBulk3.Text = "bis"
            GovTools.cbBulk1.Text = "nur Wordlist"
            GovTools.cbBulk2.Text = "volle Extraktion"

            GovTools.lbBulk.Text =
            "Bulk Extractor ist ein leistungsstarkes Werkzeug für die digitale Forensik. Es ist ein " & vbNewLine &
            "Anwendung die schnell jede Art von Input (Disk-Images, Dateien, Dateiverzeichnisse" & vbNewLine &
            "usw.) scannt und strukturierte Informationen wie E-Mail-Adressen, Kreditkartennummern," & vbNewLine &
            "JPEGs und JSON-Snippets extrahiert, ohne das Dateisystem oder Dateisystemstrukturen" & vbNewLine &
            "zu parsen. Die Ergebnisse werden in Textdateien gespeichert, die leicht inspiziert," & vbNewLine &
            "durchsucht oder als Input für andere forensische Verarbeitungen verwendet werden können." & vbNewLine &
            "Bulk Extractor erstellt auch Histogramme bestimmter Arten von Merkmalen die es findet," & vbNewLine &
            "wie bspw. Google-Suchbegriffe oder E-Mail-Adressen."

        End If

        '### PRINCE + Wordlister-Standard befüllen
        Call WordlisterX()
        Call DefaultX()

    End Sub

    Public Sub DefaultX()

        GovTools.tbPrince.Text =
                GovTools.tbPrince.Text &
            "#" & vbNewLine &
            "," & vbNewLine &
            "!" & vbNewLine &
            "!!" & vbNewLine &
            "_" & vbNewLine &
            "@" & vbNewLine &
            ":" & vbNewLine &
            "$" & vbNewLine &
            "&" & vbNewLine &
            "." & vbNewLine &
            ";" & vbNewLine &
            "+" & vbNewLine &
            "?" & vbNewLine &
            "*" & vbNewLine &
            "-" & vbNewLine &
            "/" & vbNewLine &
            "\" & vbNewLine &
            "=" & vbNewLine &
            "§" & vbNewLine &
            "'" & vbNewLine &
            "[" & vbNewLine &
            "]" & vbNewLine &
            "%" & vbNewLine &
            """" & vbNewLine &
            "^" & vbNewLine &
            "°" & vbNewLine &
            "(" & vbNewLine &
            ")" & vbNewLine &
            ">" & vbNewLine &
            "<" & vbNewLine &
            "{" & vbNewLine &
            "}" & vbNewLine &
            "~" & vbNewLine &
            "|" & vbNewLine &
            " " & vbNewLine &
            "0" & vbNewLine &
            "1" & vbNewLine &
            "2" & vbNewLine &
            "3" & vbNewLine &
            "4" & vbNewLine &
            "5" & vbNewLine &
            "6" & vbNewLine &
            "7" & vbNewLine &
            "8" & vbNewLine &
            "9" & vbNewLine &
            "11" & vbNewLine &
            "22" & vbNewLine &
            "33" & vbNewLine &
            "44" & vbNewLine &
            "55" & vbNewLine &
            "66" & vbNewLine &
            "77" & vbNewLine &
            "88" & vbNewLine &
            "99" & vbNewLine &
            "007" & vbNewLine &
            "000" & vbNewLine &
            "111" & vbNewLine &
            "012" & vbNewLine &
            "123" & vbNewLine &
            "234" & vbNewLine &
            "345" & vbNewLine &
            "456" & vbNewLine &
            "567" & vbNewLine &
            "678" & vbNewLine &
            "789" & vbNewLine &
            "222" & vbNewLine &
            "333" & vbNewLine &
            "444" & vbNewLine &
            "555" & vbNewLine &
            "666" & vbNewLine &
            "777" & vbNewLine &
            "888" & vbNewLine &
            "999" & vbNewLine &
            "1234" & vbNewLine &
            "12345" & vbNewLine &
            "123456" & vbNewLine &
            "1234567" & vbNewLine &
            "12345678" & vbNewLine &
            "123456789" & vbNewLine &
            "1000" & vbNewLine &
            "1111" & vbNewLine &
            "2222" & vbNewLine &
            "3333" & vbNewLine &
            "4444" & vbNewLine &
            "5555" & vbNewLine &
            "6666" & vbNewLine &
            "7777" & vbNewLine &
            "8888" & vbNewLine &
            "9999" & vbNewLine &
            "0815" & vbNewLine &
            "1980" & vbNewLine &
            "1981" & vbNewLine &
            "1982" & vbNewLine &
            "1983" & vbNewLine &
            "1984" & vbNewLine &
            "1985" & vbNewLine &
            "1986" & vbNewLine &
            "1987" & vbNewLine &
            "1988" & vbNewLine &
            "1989" & vbNewLine &
            "1990" & vbNewLine &
            "1991" & vbNewLine &
            "1992" & vbNewLine &
            "1993" & vbNewLine &
            "1994" & vbNewLine &
            "1995" & vbNewLine &
            "1996" & vbNewLine &
            "1997" & vbNewLine &
            "1998" & vbNewLine &
            "1999" & vbNewLine &
            "2000" & vbNewLine &
            "2001" & vbNewLine &
            "2002" & vbNewLine &
            "2003" & vbNewLine &
            "2004" & vbNewLine &
            "2005" & vbNewLine &
            "2006" & vbNewLine &
            "2007" & vbNewLine &
            "2008" & vbNewLine &
            "2009" & vbNewLine &
            "2010" & vbNewLine &
            "2011" & vbNewLine &
            "2012" & vbNewLine &
            "2013" & vbNewLine &
            "2014" & vbNewLine &
            "2015" & vbNewLine &
            "2016" & vbNewLine &
            "2017" & vbNewLine &
            "2018" & vbNewLine &
            "2019" & vbNewLine &
            "2020" & vbNewLine &
            "2021" & vbNewLine &
            "2022" & vbNewLine &
            "2023" & vbNewLine &
            "2024" & vbNewLine &
            "2025" & vbNewLine &
            "2026" & vbNewLine &
            "2027" & vbNewLine &
            "2028" & vbNewLine &
            "2029" & vbNewLine &
            "2030" & vbNewLine &
            "2031" & vbNewLine &
            "2032" & vbNewLine &
            "2033" & vbNewLine &
            "2034" & vbNewLine &
            "2035" & vbNewLine &
            "2036" & vbNewLine &
            "2037" & vbNewLine &
            "2038" & vbNewLine &
            "2039" & vbNewLine &
            "2040" & vbNewLine


    End Sub


    Public Sub WordlisterX()

        '#### Standard laden

        GovTools.tbWordStandard.Text =
                        "#" & vbNewLine &
                        "," & vbNewLine &
                        "!" & vbNewLine &
                        "!!" & vbNewLine &
                        "_" & vbNewLine &
                        "@" & vbNewLine &
                        ":" & vbNewLine &
                        "$" & vbNewLine &
                        "&" & vbNewLine &
                        "." & vbNewLine &
                        ";" & vbNewLine &
                        "+" & vbNewLine &
                        "?" & vbNewLine &
                        "*" & vbNewLine &
                        "-" & vbNewLine &
                        "/" & vbNewLine &
                        "\" & vbNewLine &
                        "=" & vbNewLine &
                        "§" & vbNewLine &
                        "'" & vbNewLine &
                        "[" & vbNewLine &
                        "]" & vbNewLine &
                        "%" & vbNewLine &
                        """" & vbNewLine &
                        "^" & vbNewLine &
                        "°" & vbNewLine &
                        "(" & vbNewLine &
                        ")" & vbNewLine &
                        ">" & vbNewLine &
                        "<" & vbNewLine &
                        "{" & vbNewLine &
                        "}" & vbNewLine &
                        "~" & vbNewLine &
                        "|" & vbNewLine &
                        " " & vbNewLine &
                        "0" & vbNewLine &
                        "1" & vbNewLine &
                        "2" & vbNewLine &
                        "3" & vbNewLine &
                        "4" & vbNewLine &
                        "5" & vbNewLine &
                        "6" & vbNewLine &
                        "7" & vbNewLine &
                        "8" & vbNewLine &
                        "9" & vbNewLine &
                        "11" & vbNewLine &
                        "22" & vbNewLine &
                        "33" & vbNewLine &
                        "44" & vbNewLine &
                        "55" & vbNewLine &
                        "66" & vbNewLine &
                        "77" & vbNewLine &
                        "88" & vbNewLine &
                        "99" & vbNewLine &
                        "007" & vbNewLine &
                        "000" & vbNewLine &
                        "111" & vbNewLine &
                        "012" & vbNewLine &
                        "123" & vbNewLine &
                        "234" & vbNewLine &
                        "345" & vbNewLine &
                        "456" & vbNewLine &
                        "567" & vbNewLine &
                        "678" & vbNewLine &
                        "789" & vbNewLine &
                        "222" & vbNewLine &
                        "333" & vbNewLine &
                        "444" & vbNewLine &
                        "555" & vbNewLine &
                        "666" & vbNewLine &
                        "777" & vbNewLine &
                        "888" & vbNewLine &
                        "999" & vbNewLine &
                        "1234" & vbNewLine &
                        "12345" & vbNewLine &
                        "123456" & vbNewLine &
                        "1234567" & vbNewLine &
                        "12345678" & vbNewLine &
                        "123456789" & vbNewLine &
                        "1000" & vbNewLine &
                        "1111" & vbNewLine &
                        "2222" & vbNewLine &
                        "3333" & vbNewLine &
                        "4444" & vbNewLine &
                        "5555" & vbNewLine &
                        "6666" & vbNewLine &
                        "7777" & vbNewLine &
                        "8888" & vbNewLine &
                        "9999" & vbNewLine &
                        "0815" & vbNewLine &
                        "1980" & vbNewLine &
                        "1981" & vbNewLine &
                        "1982" & vbNewLine &
                        "1983" & vbNewLine &
                        "1984" & vbNewLine &
                        "1985" & vbNewLine &
                        "1986" & vbNewLine &
                        "1987" & vbNewLine &
                        "1988" & vbNewLine &
                        "1989" & vbNewLine &
                        "1990" & vbNewLine &
                        "1991" & vbNewLine &
                        "1992" & vbNewLine &
                        "1993" & vbNewLine &
                        "1994" & vbNewLine &
                        "1995" & vbNewLine &
                        "1996" & vbNewLine &
                        "1997" & vbNewLine &
                        "1998" & vbNewLine &
                        "1999" & vbNewLine &
                        "2000" & vbNewLine &
                        "2001" & vbNewLine &
                        "2002" & vbNewLine &
                        "2003" & vbNewLine &
                        "2004" & vbNewLine &
                        "2005" & vbNewLine &
                        "2006" & vbNewLine &
                        "2007" & vbNewLine &
                        "2008" & vbNewLine &
                        "2009" & vbNewLine &
                        "2010" & vbNewLine &
                        "2011" & vbNewLine &
                        "2012" & vbNewLine &
                        "2013" & vbNewLine &
                        "2014" & vbNewLine &
                        "2015" & vbNewLine &
                        "2016" & vbNewLine &
                        "2017" & vbNewLine &
                        "2018" & vbNewLine &
                        "2019" & vbNewLine &
                        "2020" & vbNewLine &
                        "2021" & vbNewLine &
                        "2022" & vbNewLine &
                        "2023" & vbNewLine &
                        "2024" & vbNewLine &
                        "2025" & vbNewLine &
                        "2026" & vbNewLine &
                        "2027" & vbNewLine &
                        "2028" & vbNewLine &
                        "2029" & vbNewLine &
                        "2030" & vbNewLine &
                        "2031" & vbNewLine &
                        "2032" & vbNewLine &
                        "2033" & vbNewLine &
                        "2034" & vbNewLine &
                        "2035" & vbNewLine &
                        "2036" & vbNewLine &
                        "2037" & vbNewLine &
                        "2038" & vbNewLine &
                        "2039" & vbNewLine &
                        "2040" & vbNewLine

    End Sub

    Public Sub GovClear()

        GovTools.tbEx.Clear()
        GovTools.tbMaskPara.Clear()
        GovTools.tbMask3.Clear()
        GovTools.tbMask4.Clear()
        GovTools.tbMask5.Clear()
        GovTools.tbMask6.Clear()
        GovTools.tbMask8.Clear()
        GovTools.tbMask9.Clear()
        GovTools.tbMask11.Clear()
        GovTools.tbMask12.Clear()
        GovTools.tbMask13.Clear()
        GovTools.tbMask14.Clear()
        GovTools.tbPrince.Clear()
        GovTools.tbWordStandard.Clear()
        GovTools.tbWord1.Clear()
        GovTools.tbWord2.Clear()
        GovTools.tbWord3.Clear()
        GovTools.tbWord4.Clear()
        GovTools.tbWord5.Clear()
        GovTools.tbWord6.Clear()
        GovTools.tbWord7.Clear()
        GovTools.tbWord8.Clear()
        GovTools.tbWord9.Clear()
        GovTools.tbWord10.Clear()
        GovTools.tbWord11.Clear()
        GovTools.tbWord12.Clear()
        GovTools.tbWord13.Clear()
        GovTools.tbWord14.Clear()
        GovTools.tbWord15.Clear()
        GovTools.tbWord16.Clear()
        GovTools.tbWord17.Clear()
        GovTools.tbWord18.Clear()
        GovTools.tbWord19.Clear()
        GovTools.tbWord20.Clear()
        GovTools.tbWord21.Clear()
        GovTools.tbWord22.Clear()
        GovTools.tbWord23.Clear()
        GovTools.tbWord24.Clear()
        GovTools.tbWord25.Clear()
        GovTools.tbWord26.Clear()
        GovTools.tbWord27.Clear()
        GovTools.tbWord28.Clear()
        GovTools.tbWord29.Clear()
        GovTools.tbWord30.Clear()
        GovTools.tbWord31.Clear()
        GovTools.tbWord32.Clear()
        GovTools.tbWord33.Clear()
        GovTools.tbWord34.Clear()
        GovTools.tbWord35.Clear()
        GovTools.tbWord36.Clear()
        GovTools.tbWord37.Clear()
        GovTools.tbWord38.Clear()
        GovTools.tbWord39.Clear()
        GovTools.tbWord40.Clear()
        GovTools.tbWord41.Clear()
        GovTools.tbWord42.Clear()
        GovTools.tbWord43.Clear()
        GovTools.tbWord44.Clear()
        GovTools.tbWord45.Clear()
        GovTools.File1Txb.Clear()
        GovTools.File2Txb.Clear()
        GovTools.File3Txb.Clear()
        GovTools.LenTxb.Clear()
        GovTools.LenMinTxb.Clear()
        GovTools.LenMaxTxb.Clear()
        GovTools.tbWordScanner.Clear()
        GovTools.tbWordScanner2.Clear()
        GovTools.Duptxb.Clear()
        GovTools.DupTargettxb.Clear()
        GovTools.HashIn.Clear()
        GovTools.HashOUT.Clear()
        GovTools.tbBulkImage.Clear()
        GovTools.tbBulkmin.Clear()
        GovTools.tbBulkmax.Clear()

    End Sub

End Module
