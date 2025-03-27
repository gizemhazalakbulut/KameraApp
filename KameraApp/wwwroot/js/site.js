
document.getElementById('vertical-menu-btn').addEventListener('click', function () {
    document.querySelector('.navbar-brand-box').classList.toggle('hidden-logo');
    this.classList.toggle('menu-closed');
});

console.log("site.js doğru yüklenirse bu yazı çıkar");


//document.addEventListener("DOMContentLoaded", function () {
//    fetch("https://testdashboard.suricifatih.com/api/auth/get-device-org-tree")
//        .then(response => response.json())
//        .then(data => {
//            console.log("Gelen veri:", data);

//            const tree = buildTree(data);
//            console.log("Oluşan ağaç:", tree);

//            const container = document.getElementById("sehir-kameralari-menu");
//            container.appendChild(createMenu(tree));
//            feather.replace();
//        })
//        .catch(err => console.error("API Hatası:", err));

//    function buildTree(data) {
//        const map = {};
//        const roots = [];

//        data.forEach(item => {
//            map[item.code] = { ...item, children: [] };
//        });

//        data.forEach(item => {
//            if (item.parentCode && map[item.parentCode]) {
//                map[item.parentCode].children.push(map[item.code]);
//            } else {
//                roots.push(map[item.code]);
//            }
//        });

//        return roots;
//    }

//    function createMenu(items) {
//        const ul = document.createElement("ul");
//        ul.classList.add("sub-menu");

//        items.forEach(item => {
//            const li = document.createElement("li");
//            const a = document.createElement("a");
//            a.href = `javascript:void(0);`;
//            a.textContent = item.name;
//            a.setAttribute("data-code", item.code);

//            if (item.children.length > 0) {
//                a.classList.add("has-arrow");
//                li.appendChild(a);
//                li.appendChild(createMenu(item.children));
//            } else {
//                li.appendChild(a);
//            }

//            ul.appendChild(li);
//        });

//        return ul;
//    }
//});

//fetch("https://localhost:7176/api/test/cors-check")
//    .then(res => res.json())
//    .then(data => console.log("✅ LOCAL CORS:", data))
//    .catch(err => console.error("❌ LOCAL CORS ERROR:", err));


//fetch("https://testdashboard.suricifatih.com/api/auth/get-device-org-tree")
//    .then(res => res.json())
//    .then(data => console.log("📡 Cihaz Ağacı:", data))
//    .catch(err => console.error("❌ HATA:", err));
