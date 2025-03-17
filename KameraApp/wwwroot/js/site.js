
document.getElementById('vertical-menu-btn').addEventListener('click', function () {
    document.querySelector('.navbar-brand-box').classList.toggle('hidden-logo');
    this.classList.toggle('menu-closed');
});