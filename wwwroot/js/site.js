// Write your JavaScript code.
console.log('Groom & Glow website loaded successfully!');

// Navigation Active State Handler
document.addEventListener('DOMContentLoaded', function() {
    // Get current page path
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.nav-link');
    
    // Remove active class from all links
    navLinks.forEach(link => {
        link.classList.remove('active');
    });
    
    // Add active class to current page link
    navLinks.forEach(link => {
        const linkPath = new URL(link.href).pathname.toLowerCase();
        
        // Handle home page
        if (currentPath === '/' && linkPath === '/') {
            link.classList.add('active');
        }
        // Handle other pages
        else if (currentPath !== '/' && linkPath !== '/' && currentPath.includes(linkPath)) {
            link.classList.add('active');
        }
        // Handle exact controller matches
        else if (currentPath.includes('/services') && linkPath.includes('/services')) {
            link.classList.add('active');
        }
        else if (currentPath.includes('/booking') && linkPath.includes('/booking')) {
            link.classList.add('active');
        }
        else if (currentPath.includes('/gallery') && linkPath.includes('/gallery')) {
            link.classList.add('active');
        }
        else if (currentPath.includes('/staff') && linkPath.includes('/staff')) {
            link.classList.add('active');
        }
        else if (currentPath.includes('/about') && linkPath.includes('/about')) {
            link.classList.add('active');
        }
        else if (currentPath.includes('/contact') && linkPath.includes('/contact')) {
            link.classList.add('active');
        }
    });
});

// Add smooth scrolling for anchor links
document.addEventListener('DOMContentLoaded', function() {
    const links = document.querySelectorAll('a[href^="#"]');
    
    links.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
});

// Enhanced navigation hover effects
document.addEventListener('DOMContentLoaded', function() {
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        // Add ripple effect on click
        link.addEventListener('click', function(e) {
            const ripple = document.createElement('span');
            const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);
            const x = e.clientX - rect.left - size / 2;
            const y = e.clientY - rect.top - size / 2;
            
            ripple.style.width = ripple.style.height = size + 'px';
            ripple.style.left = x + 'px';
            ripple.style.top = y + 'px';
            ripple.classList.add('ripple');
            
            this.appendChild(ripple);
            
            setTimeout(() => {
                ripple.remove();
            }, 600);
        });
    });
});

// Add loading animation for images
function preloadImages() {
    const images = document.querySelectorAll('img[data-src]');
    images.forEach(img => {
        img.src = img.dataset.src;
        img.classList.remove('loading');
    });
}

// Initialize tooltips if Bootstrap is available
if (typeof bootstrap !== 'undefined') {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
} 