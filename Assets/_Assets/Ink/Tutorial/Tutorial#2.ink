INCLUDE tutorialData.ink

// Router
{
    - testChoice ? lemparBatu: -> batu
    - testChoice ? makan:
        { 
            - testChoice ? uang: -> main.complete
            - else: -> main.dikasihMakan
        }
    - testChoice ? uang: -> main.dikasihUang
    - testChoice ? none : -> main
    - else: -> main
}


=== main ===
#speaker:Pak Karto 
#portrait:null audio:celeste_low
Nak, minta sedekahnya..
Uhuk.
Kasihani bapak nak..
#speaker:{MC_name} 
#portrait:null #audio:animal_crossing_low
(Kasihan sekali Bapak ini.)
-> firstchoice
= firstchoice
    * {testChoice !? makan} [Beri makan]
        Nih pak, {MC_name} punya sedikit makanan untuk bapak.
        Silahkan diterima.
        
        #speaker:Pak Karto 
        #portrait:null audio:celeste_low
        Apa ini?
        Kamu kasih bapak makanan yang biasa dibeli bocah-bocah itu?
        
        #speaker:{MC_name} portrait:null #audio:animal_crossing_low
        Takoyaki namanya pak. Isi gurita.
        
        #speaker:Pak Karto
        #audio:celeste_low
        Hmmm.
        Terima kasih nak.
        ~ testChoice += makan
        ~ testChoice += dikasih
        -> DONE
    * {testChoice !? uang} [Kasih uang]
        Nih pak, {MC_name} cuma punya uang segini.
        
        #speaker:Pak Karto 
        #portrait:null audio:celeste_low
        Terima kasih banyak nak, semoga rejekinya bertambah.
        ~ testChoice += uang
        ~ testChoice += dikasih
        -> DONE
    * [Lempar batu]
        #speaker:null #audio:null
        Kamu membungkuk dan mengambil batu kecil di tanah.
        Kamu melempar batu tersebut ke arah kaki bapak tersebut dan sontak berpura-pura dengembalikan posisi tangan ke keadaan semula.
        Bapak tersebut kaget.
        
        #speaker:Pak Karto 
        #portrait:null audio:celeste_low
        WEIK! JUANGKRIK!
        
        #speaker:null #audio:null
        Bapak tersebut berusaha mencari orang yang melempar batu tersebut, dan kamu masih berusaha agar bersikap kalem.
        Tak lama bapak tersebut kembali duduk di tanah dan kembali mengemis.
        ~ testChoice += lemparBatu
        -> DONE
    * {testChoice !? dikasih} [Biarin]
        #speaker:null #audio:null
        Kamu mencoba untuk mengabaikan orang itu dengan mengacuhkan pandangan ke arah lain.
        -> DONE
    * {testChoice ? dikasih} [Pergi]
        -> DONE

= dikasihMakan
#speaker:Pak Karto
#portrait:null #audio:celeste_low
Terima kasih mas, jajannya.
-> main.firstchoice

= dikasihUang
#speaker:PakKarto
#portrait:null #audio:celeste_low
Ada apa lagi nak?
-> main.firstchoice

= complete
#speaker:Pak Karto #portrait:null #audio:celeste_low
Bapak sangat bersyukur atas kebaikanmu nak. Terima kasih banyak.
-> DONE

=== batu ===
#speaker:Pak Karto
#portrait:null #audio:celeste_low
Ada orang mengemis berani-beraninya dilempari batu.
Dikira orang gila atau gimana? Orang kesusahan bukannya dibantu malah dilempar-lempari batu.
Orang tidak punya moral memang.
-> END
