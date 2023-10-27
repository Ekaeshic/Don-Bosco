INCLUDE DialogueData.ink

{
- suhadi == false: -> once
}

#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Hmm... mungkin aku juga akan bergabung.
-> END

=== once ===
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Kau dengar itu Karman? mereka akan membentuk pasukan polisi baru!
Kalau tidak salah, namanya Badan Keamanan Rakjat?
* [Iya, benar]
-
Padahal setelah PETA dibubarkan minggu lalu, kita akhirnya dapat pulang ke kota Surabaya.
Ahh... Setelah sekian lama akhirnya aku bisa memakan masakan Ibuku...
Kau tahu? Saat kutinggal pergi bertugas, Ibuku mencoba untuk membuka usaha jasa jahit pakaian.
Akhir-akhir ini aku membantu usaha Ibu. Beberapa hari ini ada setidaknya 7 pesanan yang harus diselesaikan.
Aku hanya bisa menyelesaikan 2 pakaian saja dalam sehari. Hah... Aku jadi merasa kecewa pada diriku sendiri.
Hebat sekali Ibu bisa menyelesaikan semua pesanan itu seorang diri selama ini.
Aku ingin membantu meneruskan usaha Ibu dan hidup dengan santai.
Namun sepertinya hal itu tidak memungkinkan untuk saat ini, hahaha...
* [Menurutku ini adalah hal yang bagus]
    #speaker:Karman #portrait:null #audio:alphabet
    Lambat laun, kita membutuhkan suatu aparat keamanan untuk menjaga kemerdekaan ini.
    #speaker:Suhadi
    Hmmm... begitukah?
    Aku memang merasa senang bisa membantu Ibuku, 
    Tetapi sejujurnya, aku juga masih sangat ingin berkontribusi dalam menjaga kemerdekaan kita.
    Mungkin ada cara untuk melakukannya sambil tetap mendukung Ibuku di usaha jahitnya.
    Bagaimana menurutmu, apakah bung akan bergabung?
    * * [Aku pikir aku akan mencoba bergabung BKR]
        -> Gabung
    * * [Akan ku pikirkan nanti]
        -> done
* [Aku pikir aku akan mencoba bergabung BKR]
    -> Gabung
    = Gabung
    Bagus, Karman. 
    Aku yakin dengan keterampilan dan semangatmu, kau akan menjadi aset yang berharga bagi Badan Keamanan Rakjat.
    ~ suhadi = true
    -> END
= done
Kalau bung ingin bergabung, pergilah ke kantor BKR secara langsung.
~ suhadi = true
-> END

=== selebaran ===
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Ada apa bung? kenapa kembali lagi?
#speaker:Karman #portrait:null #audio:alphabet
Bung, baru saja ada pesawat Belanda mengirimkan selebaran.
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Selebaran?! Sebentar, Aku akan keluar sebentar lagi untuk melihat-lihat.
Aku mau ganti baju terlebih dahulu.
#speaker:Karman #portrait:null #audio:alphabet
Baiklah, Kalau begitu Aku akan pergi ke kantor BKR untuk melaporkan hal ini.
-> END