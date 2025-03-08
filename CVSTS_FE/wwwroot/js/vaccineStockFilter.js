document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("applyFiltersBtn")?.addEventListener("click", applyFilters);
    document.getElementById("resetFiltersBtn")?.addEventListener("click", resetFilters);
});

function applyFilters() {
    let vaccineName = document.getElementById("vaccineNameFilter").value.trim();
    let minQuantity = document.getElementById("minQuantityFilter").value;
    let maxQuantity = document.getElementById("maxQuantityFilter").value;
    let expiryStartDate = document.getElementById("expiryStartDateFilter").value;
    let expiryEndDate = document.getElementById("expiryEndDateFilter").value;

    let filters = [];

    if (vaccineName) {
        filters.push(`contains(Vaccine/Name, '${vaccineName}')`);
    }
    if (minQuantity) {
        filters.push(`Quantity ge ${minQuantity}`);
    }
    if (maxQuantity) {
        filters.push(`Quantity le ${maxQuantity}`);
    }
    if (expiryStartDate) {
        const startDate = new Date(expiryStartDate).toISOString().split('T')[0];
        filters.push(`ExpiryDate ge ${startDate}`);
    }
    if (expiryEndDate) {
        const endDate = new Date(expiryEndDate).toISOString().split('T')[0];
        filters.push(`ExpiryDate le ${endDate}`);
    }

    let query = filters.length ? `$filter=${filters.join(" and ")}` : "";

    document.getElementById("odataQuery").value = query;
    document.getElementById("filterForm").submit();
}

function resetFilters() {
    document.getElementById("vaccineNameFilter").value = "";
    document.getElementById("minQuantityFilter").value = "";
    document.getElementById("maxQuantityFilter").value = "";
    document.getElementById("expiryStartDateFilter").value = "";
    document.getElementById("expiryEndDateFilter").value = "";

    document.getElementById("odataQuery").value = "";
    document.getElementById("filterForm").submit();
}