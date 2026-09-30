// === 3D TILT EFFECT FOR CARDS ===
const tiltCards = document.querySelectorAll('.problem-card, .benefit-card, .audience-card, .step-card');

tiltCards.forEach(card => {
    card.addEventListener('mousemove', (e) => {
        const rect = card.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;
        
        // Вычисляем проценты для наклона (от -10 до 10 градусов)
        const centerX = rect.width / 2;
        const centerY = rect.height / 2;
        const rotateX = ((y - centerY) / centerY) * -8; 
        const rotateY = ((x - centerX) / centerX) * 8;

        card.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) scale3d(1.02, 1.02, 1.02)`;
        
        // Двигаем блик
        const bgX = (x / rect.width) * 100;
        const bgY = (y / rect.height) * 100;
        card.style.setProperty('--mouse-x', `${bgX}%`);
        card.style.setProperty('--mouse-y', `${bgY}%`);
    });

    card.addEventListener('mouseleave', () => {
        card.style.transform = 'perspective(1000px) rotateX(0) rotateY(0) scale3d(1, 1, 1)';
    });
});

// === STAGGER TEXT REVEAL FOR H1 ===
document.querySelectorAll('.hero__title').forEach(title => {
    const text = title.innerHTML;
    // Разбиваем по пробелам, сохраняя HTML теги (грубо, но эффективно для спанов)
    const words = text.split(/\s+/);
    title.innerHTML = words.map((word, i) => {
        // Если слово содержит <span>, обворачиваем аккуратно
        return `<span class="stagger-word" style="transition-delay: ${i * 0.08}s">${word}</span>`;
    }).join(' ');
    
    // Триггерим появление
    setTimeout(() => {
        document.querySelectorAll('.stagger-word').forEach(w => w.classList.add('visible'));
    }, 300);
});