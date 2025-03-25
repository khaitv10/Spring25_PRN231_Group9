document.addEventListener('DOMContentLoaded', function () {
    const rows = document.querySelectorAll('tbody tr[id^="vaccineRow-"]');

    rows.forEach(row => {
        row.addEventListener('click', function () {
            const id = this.id.split('-')[1];
            window.location.href = `/Staff/VaccineManage/Details?id=${id}`;
        });
    });
});