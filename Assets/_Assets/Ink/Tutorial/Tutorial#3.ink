INCLUDE tutorialData.ink

VAR sceneName = "Balaikota"
EXTERNAL PindahScene(sceneName)
EXTERNAL TidakMauInteraksi(i)

#speaker:Pak guru #portrait:null #audio:pak_pratama
Selamat pagi mas
    * [Selamat pagi pak]
        #speaker:Arul #portrait:null #audio:alphabet
        Selamat pagi pak
        -> DONE
    + [Pergi ke balai kota]
        ~ PindahScene("BalaiKota")
        -> DONE
    * [Tidak mau interaksi lagi]
        ~ interactable = false
        ~ TidakMauInteraksi("")
    + [Bye bye]
        -> DONE

-> END
