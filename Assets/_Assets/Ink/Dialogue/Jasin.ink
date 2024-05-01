INCLUDE DialogueData.ink

#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Selamat siang, ada yang bisa saya bantu bung?
-> END

=== GiveSelebaran ===
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Selamat siang, ada yang bisa saya bantu bung?
* [Ada pesawat Belanda membagikan selebaran]
-
#speaker:Karman #portrait:karman #audio:alphabet
Selamat siang bung, Baru saja ada pesawat Belanda membagikan selebaran di langit Surabaya.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Ya. Saya sudah mendengar kabar tersebut.
Sepertinya pasukan sekutu akan datang ke Surabaya.
Tapi Saya belum membacanya isi selebarannya.
Apakah bung mempunyai kertas selebarannya?
#speaker:Karman #portrait:karman #audio:alphabet
Ya, saya membawanya.
* [Berikan selebaran]
-
#speaker:null #portrait:null #audio:null
Kau memberikan selebaran kepada Bung Jasin.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
...
Hmm, menjemput para interniran?
Kita harus mengadakan rapat mengenai hal ini.
Hei! panggil para atasan BKR untuk berkumpul sore ini.
Oh, panggil juga Bung Tomo.
#speaker:Prajurit Gelisah #portrait:pejuang #audio:pak_karto
Baik pak!
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
...
Bung, siapa namamu?
* [Karman]
-
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Baiklah, bung Karman. Sepertinya bung kesini tidak hanya untuk memberikan selebaran ini kan?
* [Saya ingin tahu lebih lanjut mengenai BKR]
    Badan Keamanan Rakjat atau disingkat BKR dibentuk untuk melakukan tugas pemeliharaan keamanan.
    Sebenarnya, BKR adalah bagian dari Badan Penolong Keluarga Korban Perang, atau BPKKP.
    Sehingga berbeda dengan polisi militer milik Jepang, disini kita bergerak bersama-sama dengan rakyat.
    Para pemuda biasa yang memiliki semangat untuk menjaga kemerdekaan dapat bergabung dan akan dilatih disini.
    Bung Karno bersama para anggota PPKI pada awalnya ingin membentuk badan tentara militer.
    Untuk mengisi kekuatan tempur milik Indonesia, sekaligus untuk menjaga keamanan.
    Namun banyak pihak yang menentang hal ini, dikarenakan masih tentara milik Jepang masih aktif saat itu.
    Akhirnya pada tanggal 23 Agustus, dibentuklah BKR yang memiliki tugas untuk menjaga keamanan dan mengayomi rakjat.
    Esoknya tanggal 24 Agustus, Dr. Moestopo dan bawahannya membentuk BKR di Kota Surabaya yang kita injak saat ini.
    Mas Piet, ehem. Bung Hario Jonosewojo yang saat ini menjadi kepala sekuriti juga ikut pada rapat pembentukkan saat itu.
    Ehem...
    Cukup penjelasannya, apakah bung sudah puas?
    ~ infoJasin += BKR
    * * [Kalau begitu saya akan bergabung]
        -> Gabung
    * * [Bagaimana dengan bung sendiri?]
        Saya? Kalau begitu perkenalkan. Nama Saya Mochammad Jasin.
        Lahir di Sabang, tetapi memiliki darah Jawa dari bapak saya, dan lulusan taman siswa Ki Hajar Dewantara di Jogja.
        Mantan perwira daidan 1 PETA di Madiun, kemudian pergi ke Surabaya untuk bergabung dengan BKR.
        Saat ini menjabat sebagai letnan dan bertugas untuk merekrut para pemuda untuk bergabung.
        Namun tidak lama lagi, saya akan ditugaskan untuk merekrut para pemuda di kota Madiun untuk mendirikan BKR disana.
        Meskipun seperinya akan tertunda, jika apa yang ada di selebaran ini benar adanya...
        ~ infoJasin += jasin
        * * * [Kalau begitu saya akan bergabung]
            -> Gabung
* [Saya kesini untuk bergabung]
    -> Gabung
= Gabung
#speaker:Karman #portrait:karman #audio:alphabet
Saya adalah mantan anggota pasukan PETA di Blitar.
Saya kesini untuk bergabung menjadi anggota BKR.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Hahaha! Baiklah. Dengan semangat itu, bagaimana mungkin saya menolakmu.
...
Dengan ini, karena bung juga yang telah memberitahuku mengenai selebaran itu.
Hari ini, bung telah menjadi anggota BKR secara resmi.
Saya harap nanti bung siap untuk mengikuti rapat untuk membahas tindak lanjut atas selebaran ini.
#speaker:Karman #portrait:karman #audio:alphabet
Ya! Terima kasih atas pertimbangannya.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Bicaralah padaku apabila bung telah siap untuk mengikuti rapat ini.
-> END

=== StartRapat ===
EXTERNAL ChangeScene(sceneName)
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Bagaimana? Apakah bung telah siap untuk mengikuti rapat ini?
+ {infoJasin !? BKR}[Saya ingin tahu lebih lanjut mengenai BKR]
    Badan Keamanan Rakjat atau disingkat BKR dibentuk untuk melakukan tugas pemeliharaan keamanan.
    Sebenarnya, BKR adalah bagian dari Badan Penolong Keluarga Korban Perang, atau BPKKP.
    Sehingga berbeda dengan polisi militer milik Jepang, disini kita bergerak bersama-sama dengan rakyat.
    Para pemuda biasa yang memiliki semangat untuk menjaga kemerdekaan dapat bergabung dan akan dilatih disini.
    Bung Karno bersama para anggota PPKI pada awalnya ingin membentuk badan tentara militer.
    Untuk mengisi kekuatan tempur milik Indonesia, sekaligus untuk menjaga keamanan.
    Namun banyak pihak yang menentang hal ini, dikarenakan masih tentara milik Jepang masih aktif saat itu.
    Akhirnya pada tanggal 23 Agustus, dibentuklah BKR yang memiliki tugas untuk menjaga keamanan dan mengayomi rakjat.
    Esoknya tanggal 24 Agustus, Dr. Moestopo dan bawahannya membentuk BKR di Kota Surabaya yang kita injak saat ini.
    Mas Piet, ehem. Bung Hario Jonosewojo yang saat ini menjadi kepala sekuriti juga ikut pada rapat pembentukkan saat itu.
    Ehem...
    Cukup penjelasannya, apakah bung sudah puas?
    ~ infoJasin += BKR
    -> StartRapat
+ {infoJasin !? jasin}[Saya ingin mengenal bung lebih jauh]
    Saya? Kalau begitu perkenalkan. Nama Saya Mochammad Jasin.
    Lahir di Sabang, tetapi memiliki darah Jawa dari bapak saya, dan lulusan taman siswa Ki Hajar Dewantara di Jogja.
    Mantan perwira daidan 1 PETA di Madiun, kemudian pergi ke Surabaya untuk bergabung dengan BKR.
    Saat ini menjabat sebagai letnan dan bertugas untuk merekrut para pemuda untuk bergabung.
    Namun tidak lama lagi, saya akan ditugaskan untuk merekrut para pemuda di kota Madiun untuk mendirikan BKR disana.
    Meskipun seperinya akan tertunda, jika apa yang ada di selebaran ini benar adanya...
    ~ infoJasin += jasin
    -> StartRapat
* [Saya siap]
    Baiklah. Kalau begitu rapat ini akan dimulai.
    ~ ChangeScene("ARC2_PERSIAPAN")
    -> END
* [Belum siap]
    Ya, bicaralah padaku apabila bung telah siap untuk mengikuti rapat ini.
    -> END
-> END

=== PesawatSelebaran ===
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Selamat siang, ada yang bisa saya bantu bung?
#speaker:Karman #portrait:karman #audio:alphabet
Selamat siang bung, Baru saja ada pesawat Belanda membagikan selebaran di langit Surabaya.
#speaker:Moh. Jasin #audio:m_yasin #portrait:m_yasin
Ya. Saya sudah mendengar kabar tersebut.
Sepertinya pasukan sekutu akan datang ke Surabaya.
Tapi Saya belum membacanya isi selebarannya.
Apakah bung mempunyai kertas selebarannya?
#speaker:Karman #portrait:karman #audio:alphabet
Oh, maaf Saya tidak membawanya.
Akan saya bawakan untuk bung.
-> END