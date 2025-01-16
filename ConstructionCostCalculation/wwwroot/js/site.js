const toggleButton = document.getElementById('modeToggle');

toggleButton.addEventListener('click', () => {
    var theme = document.documentElement.getAttribute('data-bs-theme');
    if (theme === 'dark') {
        

        document.documentElement.setAttribute('data-bs-theme', 'light');
        toggleButton.textContent = 'Dark Mode';
    }
    else {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
        toggleButton.textContent = 'Light Mode';
    }

});