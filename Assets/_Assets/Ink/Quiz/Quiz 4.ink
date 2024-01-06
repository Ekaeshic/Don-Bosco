EXTERNAL Quiz(id_answer)
#speaker:null #audio:null #portrait:null
Perjuanganmu pada penyerangan di gudang Don Bosco mendapat perhatian dari para petinggi BKR
Karman, telah diangkat menjadi perwira dan bertugas untuk mengolah intel di BKR
Terdapat banyak pertanyaan yang terpikirkan olehmu
Bagaimana jika sejak awal Indonesia tidak dijajah?
Apa jadinya Indonesia tanpa para pejuang yang telah berjasa besar?
Bagaimana jika Indonesia tidak pernah mendapat kemerdekaan?
Perundingan di Don Bosco bersama atasan Jepang juga memunculkan pertanyaan di kepalamu
Seberapa besar persenjataan yang diambil oleh para pejuang?
* Semua jenis persenjataan
    ~ Quiz("4_1")
    Benar, para pejuang mengambil semua persenjataan tanpa terkecuali
    -> quiz5
* Beberapa senapan dan amunisi
    ~ Quiz("4_2")
    Tidak, jawabanmu salah
    Para pejuang mengambil semua persenjataan tanpa terkecuali
    -> quiz5
    
//DELETE
=== quiz6 ===
Mengapa Mayor Hashimoto tidak ingin menyerahkan katana miliknya?
* Karena katana itu sangat mahal
    ~ Quiz("6_1")
    Mungkin saja, namun jawabanmu salah
    Mayor Hashimoto mengklaim bahwa katana itu adalah warisan keluarga yang berharga
    -> quiz6
* Karena katana itu berharga baginya
    ~ Quiz("6_2")
    Benar, Mayor Hashimoto mengklaim bahwa katana itu adalah warisan keluarga yang berharga
    -> quiz6

=== quiz5 ===
Bagaimana bung Soetomo dapat berbicara bahasa Jepang?
* Pernah pergi ke Jepang
    ~ Quiz("5_1")
    Jawabanmu salah
    Bung Tomo pernah menjadi wartawan Domei milik Jepang
    -> ending
* Berpengalaman menjadi wartawan Jepang
    ~ Quiz("5_2")
    Benar, Bung Tomo pernah menjadi wartawan Domei milik Jepang
    -> ending
    
    
=== ending ===
Terdapat berbagai pertanyaan yang masih terpikirkan dan belum dapat dipecahkan
Kamu tidak mempunyai banyak waktu untuk memikirkan semua hal tersebut
Kehidupan Karman terus berlanjut
Begitu pula semangat juang untuk mempertahankan kemerdekaan Indonesia
-> END