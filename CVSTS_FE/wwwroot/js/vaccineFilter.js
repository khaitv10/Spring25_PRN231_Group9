document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("applyFiltersBtn")?.addEventListener("click", applyFilters);
    document.getElementById("resetFiltersBtn")?.addEventListener("click", resetFilters);
});

function applyFilters() {
    let name = document.getElementById("nameFilter").value.trim();
    let minAge = document.getElementById("minAgeFilter").value;
    let maxAge = document.getElementById("maxAgeFilter").value;
    let status = document.getElementById("statusFilter").value;

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
    if (status) {
        filters.push(`Status eq '${status}'`);
    }

    let query = filters.length ? `$filter=${filters.join(" and ")}` : "";

    // Store the filter query in a hidden input field
    document.getElementById("odataQuery").value = query;

    // Submit the form
    document.getElementById("filterForm").submit();
}

function resetFilters() {
    document.getElementById("nameFilter").value = "";
    document.getElementById("minAgeFilter").value = "";
    document.getElementById("maxAgeFilter").value = "";
    document.getElementById("statusFilter").value = "";

    // Clear the hidden field
    document.getElementById("odataQuery").value = "";

    // Submit form to reset filters
    document.getElementById("filterForm").submit();
}
