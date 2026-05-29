document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("SearchInput");

    const tableBody = document.querySelector(".innovative-table tbody");
    const noDataAlert = document.getElementById("noDataAlert");

    if (!searchInput || !tableBody) return;

    const rows = tableBody.getElementsByTagName("tr");

    searchInput.addEventListener("input", function () {
        const filter = this.value.toLowerCase().trim();
        let visibleCount = 0;

        Array.from(rows).forEach(row => {
            const rowText = row.textContent.toLowerCase();

            if (rowText.includes(filter)) {
                row.style.display = ""; 
                visibleCount++;
            } else {
                row.style.display = "none"; 
            }
        });

   
        const tableElement = tableBody.closest("table");
        if (noDataAlert) {
            if (visibleCount === 0) {
                noDataAlert.classList.remove("d-none");
                if (tableElement) tableElement.style.display = "none";
            } else {
                noDataAlert.classList.add("d-none");
                if (tableElement) tableElement.style.display = "table";
            }
        }
    });
});