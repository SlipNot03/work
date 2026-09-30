// Scroll reveal — with robust fallback for in-app browsers (Telegram, etc.)
  (function() {
    var reveals = document.querySelectorAll('.reveal');

    if (typeof IntersectionObserver === 'undefined') {
      document.documentElement.classList.add('no-observer');
      reveals.forEach(function(el) { el.classList.add('visible'); });
      return;
    }

    try {
      var observer = new IntersectionObserver(function(entries) {
        entries.forEach(function(entry) {
          if (entry.isIntersecting) {
            entry.target.classList.add('visible');
          }
        });
      }, { threshold: 0.1, rootMargin: '0px 0px -50px 0px' });

      reveals.forEach(function(el) { observer.observe(el); });

      setTimeout(function() {
        reveals.forEach(function(el) { el.classList.add('visible'); });
      }, 2000);
    } catch (e) {
      document.documentElement.classList.add('no-observer');
      reveals.forEach(function(el) { el.classList.add('visible'); });
    }
  })();

  // Live EUR→USDT rate for the invoice illustration (falls back to static if offline/blocked)
  (function() {
    var AMOUNT_EUR = 100000;
    var rateEl = document.getElementById('liveRate');
    var paidEl = document.getElementById('paidIn');
    var tagEl  = document.getElementById('liveTag');
    if (!rateEl || !paidEl) return;

    function fmt(n) {
      return n.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    fetch('https://api.coingecko.com/api/v3/simple/price?ids=tether&vs_currencies=eur')
      .then(function(r) { return r.json(); })
      .then(function(d) {
        var usdtInEur = d && d.tether && d.tether.eur;
        if (!usdtInEur) return;
        var eurToUsdt = 1 / usdtInEur;            // 1 EUR = X USDT
        rateEl.textContent = '1 EUR = ' + eurToUsdt.toFixed(4) + ' USDT';
        paidEl.textContent = 'USDT \u00B7 ' + fmt(AMOUNT_EUR * eurToUsdt);
        if (tagEl) tagEl.classList.add('on');
      })
      .catch(function() { /* keep static fallback values */ });
  })();

  document.querySelectorAll('.benefit').forEach(card => {
    card.addEventListener('mousemove', (e) => {
      const rect = card.getBoundingClientRect();
      const x = ((e.clientX - rect.left) / rect.width) * 100;
      const y = ((e.clientY - rect.top) / rect.height) * 100;
      card.style.setProperty('--mx', x + '%');
      card.style.setProperty('--my', y + '%');
    });
  });
