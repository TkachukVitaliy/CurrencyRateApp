const selectElement = document.querySelector('#select');

const selectedValue = localStorage.getItem('selectedCurrency');


if (selectedValue !== null) {
    selectElement.value = selectedValue;
}


selectElement.addEventListener('change', (event) => {
    localStorage.setItem('selectedCurrency', event.target.value);
});
}

function SaveCurrencyRate(selectElement) {
    const selectedValue = localStorage.getItem('selectedCurrency');

    if (selectedValue !== null) {
        selectElement.value = selectedValue;
    }

    selectElement.addEventListener('change', (event) => {
        localStorage.setItem('selectedCurrency', event.target.value);
    });
}
