function showContent(section) {
    // Hide all sections
    const sections = document.querySelectorAll('.content-section');
    sections.forEach(sec => sec.classList.remove('active'));
    
    // Show the selected section
    document.getElementById(section).classList.add('active');
}

// Initial display
document.getElementById('orders').classList.add('active');
