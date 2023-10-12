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
Padahal setelah PETA dibubarkan minggu lalu, kita akhirnya dapat pulang ke kota Surabaya.
Ahh... Setelah sekian lama akhirnya aku bisa memakan masakan Ibuku...
Kau tahu? Saat kutinggal pergi bertugas, Ibuku mencoba untuk membuka usaha jasa jahit pakaian.
Akhir-akhir ini aku membantu usaha Ibu. Beberapa hari ini ada setidaknya 7 pesanan yang harus diselesaikan.
Aku hanya bisa menyelesaikan 2 pakaian saja dalam sehari. Hah... Aku jadi merasa kecewa pada diriku sendiri.
Hebat sekali Ibu bisa menyelesaikan semua pesanan itu seorang diri selama ini.
Aku ingin membantu meneruskan usaha Ibu dan hidup dengan santai.
Namun sepertinya hal itu tidak memungkinkan untuk saat ini, hahaha...
Apakah menurutmu BKR ini memang perlu dibentuk?
#speaker:Karman #portrait:null #audio:alphabet
Hm... menurutku ini adalah hal yang bagus. Lambat laun, kita membutuhkan suatu aparat keamanan untuk menjaga kemerdekaan ini.
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
Hmmm... begitukah?
Bagaimana menurutmu, apakah bung akan bergabung?
#speaker:Karman #portrait:null #audio:alphabet
Yah bagimanapun, kita berdua sebagai mantan pasukan PETA mempunyai pengalaman.
Menurutku tidak ada salahnya untuk mencoba bergabung ke badan organisasi tersebut.
#speaker:Suhadi #portrait:null #audio:animal_crossing_low
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