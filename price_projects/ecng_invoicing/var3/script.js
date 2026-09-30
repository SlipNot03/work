/**
 * ECNG Invoicing - Variant 3
 * - Sticky nav state
 * - Scroll reveal
 * - CTA analytics stub
 */

document.addEventListener('DOMContentLoaded', () => {
    initNavigation();
    initScrollReveal();
    initAnalytics();
});

function initNavigation() {
    const navbar = document.getElementById('navbar');
    if (!navbar) return;

    window.addEventListener('scroll', () => {
        navbar.classList.toggle('scrolled', window.scrollY > 40);
    }, { passive: true });
}

function initScrollReveal() {
    const revealElements = document.querySelectorAll('[data-reveal]');

    if (!('IntersectionObserver' in window)) {
        revealElements.forEach(el => el.classList.add('visible'));
        return;
    }

    const revealObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const delay = Number(entry.target.dataset.delay || 0);
                setTimeout(() => entry.target.classList.add('visible'), delay * 160);
                revealObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.15 });

    revealElements.forEach(el => revealObserver.observe(el));
}

function trackEvent(eventName, payload = {}) {
    console.log(`[Analytics] ${eventName}`, payload);
}

function initAnalytics() {
    document.querySelectorAll('.btn').forEach(btn => {
        btn.addEventListener('click', () => {
            trackEvent('cta_click', {
                button_id: btn.id || null,
                text: btn.innerText.trim(),
                url: btn.getAttribute('href')
            });
        });
    });

    document.querySelectorAll('a[href*="t.me"]').forEach(link => {
        link.addEventListener('click', () => {
            trackEvent('telegram_redirect');
        });
    });
}
