document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("applyFiltersBtn")?.addEventListener("click", applyFilters);
    document.getElementById("resetFiltersBtn")?.addEventListener("click", resetFilters);
});

function applyFilters() {
    let name = document.getElementById("nameFilter").value.trim();
    let minAge = document.getElementById("minAgeFilter").value;
    let maxAge = document.getElementById("maxAgeFilter").value;

    let filters = [];

    if (name) {
        filters.push(`contains(Name, '${name}')`);
    }
    if (minAge) {
        filters.push(`MinAge ge ${minAge}`);
    }
    if (maxAge) {
        filters.push(`MaxAge le ${maxAge}`);
    }

    let query = filters.length ? `$filter=${filters.join(" and ")}` : "";

    document.getElementById("odataQuery").value = query;
    document.getElementById("filterForm").submit();
}

function resetFilters() {
    document.getElementById("nameFilter").value = "";
    document.getElementById("minAgeFilter").value = "";
    document.getElementById("maxAgeFilter").value = "";

    document.getElementById("odataQuery").value = "";
    document.getElementById("filterForm").submit();
}