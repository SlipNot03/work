// script.js

// === ANALYTICS STUBS ===
// Replace with actual GA4, Facebook Pixel, or custom analytics implementation
function trackEvent(eventName, payload = {}) {
    console.log(`[Analytics Event] ${eventName}`, payload);
    
    // Example integration placeholder:
    // if (typeof gtag === 'function') {
    //     gtag('event', eventName, payload);
    // }
    // if (typeof fbq === 'function') {
    //     fbq('trackCustom', eventName, payload);
    // }
}

// === SCROLL REVEAL ANIMATION ===
document.addEventListener('DOMContentLoaded', () => {
    const revealElements = document.querySelectorAll('.reveal');

    const observerOptions = {
        root: null,
        rootMargin: '0px',
        threshold: 0.1
    };

    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);

    revealElements.forEach(el => observer.observe(el));
});

// === SMOOTH SCROLL FOR ANCHOR LINKS ===
// Native CSS scroll-behavior: smooth is used, but adding JS fallback for older browsers
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        const targetId = this.getAttribute('href');
        if (targetId === '#') return;
        
        const targetElement = document.querySelector(targetId);
        if (targetElement) {
            e.preventDefault();
            targetElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    });
});
