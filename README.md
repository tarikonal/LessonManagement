# Özel Ders API Uygulaması

## Açıklama

Bu API, özel ders alan öğrencilerin ve ders veren öğretmenlerin verilerini yönetmeyi amaçlayan bir uygulamadır. Aileler, bireylere aldırdıkları dersleri ve bu derslerin ücretlerini takip edebilirken, öğretmenler de soyadı ve aile bilgilerini kullanarak öğrencilerini kayıt altına alabilir ve verdiği özel derslerin ücretlerini tutabilir.

---

## Özellikler

- **Öğrenci Takibi:** 
    - Öğrencilerin ders bilgilerini ve özel ders aldıkları öğretmenleri kayıt altına alma.
    - Öğrencilerin aldığı özel derslerin detaylarını (dersin konusu, süresi, tarih ve saat bilgisi) tutma.

- **Öğretmen Takibi:**
    - Özel ders veren öğretmenlerin soyadı ve aile bilgileri girilerek öğrenci kayıtlarını yapma.
    - Öğretmenlerin verdikleri derslerin ücretlerini kaydetme ve yönetme.

- **Aileler için:** 
    - Ailelerin, bireylere aldırdıkları özel dersleri ve bu derslerin ücretlerini takip edebileceği bir yapı sunar.
    - Ödeme işlemleri ve ders detaylarını aileler üzerinden kolayca izlenebilir hale getirir.

---

## API Yapısı

### Endpointler

- **/api/student**: Öğrencilerin bilgilerini tutan endpoint.
    - GET, POST, PUT, DELETE işlemleri ile öğrencilerin verilerini yönetebilirsiniz.
    
- **/api/teacher**: Özel ders veren öğretmenlerin bilgilerini yöneten endpoint.
    - GET, POST, PUT, DELETE işlemleri ile öğretmen kayıtlarını yönetebilirsiniz.

- **/api/lesson**: Verilen özel derslerin takibini yapan endpoint.
    - Öğrenciler ve öğretmenler arasındaki derslerin detaylarını (dersin süresi, konusu, ücreti vb.) yönetir.
    
- **/api/family**: Aileler için bireylerin aldıkları dersleri ve ücretlerini izlemelerine olanak tanır.
    - Derslerin ödemelerini ve ödeme tarihlerini izleyebilirler.

---
