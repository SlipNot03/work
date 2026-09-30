<script setup>
import { onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import AppFooter from './components/AppFooter.vue'
import AppHeader from './components/AppHeader.vue'
import CommentsPanel from './components/CommentsPanel.vue'

const route = useRoute()

const initRevealAnimations = () => {
  const revealElements = document.querySelectorAll('.reveal:not(.active)')

  if (revealElements.length === 0) return

  const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add('active')
        observer.unobserve(entry.target)
      }
    })
  }, {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px',
  })

  revealElements.forEach((el) => observer.observe(el))
}

const motionSelector = [
  '.about-grid article',
  '.page-intro',
  '.lead-text',
  '.article-page > img',
  '.article-meta .category',
  '.share-links a',
  '.comments-panel__sticky',
  '.comment-card',
  '.contacts-info',
  '.contact-form',
  '.hero-image',
  '.ad-slot',
].join(', ')

const getMotionProfile = (element) => {
  if (element.matches('.comments-panel__sticky, .page-intro')) {
    return { lift: -6, drift: 3, tilt: 0.8, scale: 1.008, depth: 0.45, follow: 0.065, kind: 'panel' }
  }

  if (element.matches('.comment-card, .article-meta .category, .share-links a')) {
    return { lift: -5, drift: 5, tilt: 1.25, scale: 1.016, depth: 0.58, follow: 0.08, kind: 'compact' }
  }

  if (element.matches('.contacts-info, .contact-form')) {
    return { lift: -12, drift: 8, tilt: 1.8, scale: 1.025, depth: 0.78, follow: 0.07, kind: 'panel' }
  }

  if (element.matches('.article-page > img, .hero-image')) {
    return { lift: -18, drift: 10, tilt: 2, scale: 1.026, depth: 0.9, follow: 0.065, kind: 'media' }
  }

  return { lift: -16, drift: 9, tilt: 2.4, scale: 1.034, depth: 0.92, follow: 0.065, kind: 'card' }
}

const getMotionShadow = (profile, motion) => {
  const depth = Math.max(0, Math.min(1, motion.depth))
  const hardAlpha = (depth * 0.18).toFixed(3)
  const glowAlpha = (depth * 0.2).toFixed(3)
  const darkAlpha = (depth * 0.13).toFixed(3)
  const x = (motion.x * -0.8).toFixed(2)
  const y = Math.max(0, depth * 18).toFixed(2)
  const blur = Math.max(0, depth * 54).toFixed(2)

  if (profile.kind === 'compact') {
    return `${x}px ${y}px 28px rgba(255, 77, 109, ${hardAlpha}), 0 ${Math.max(0, depth * 16).toFixed(2)}px 42px rgba(255, 77, 109, ${glowAlpha})`
  }

  if (profile.kind === 'media') {
    return `0 ${Math.max(12, depth * 28).toFixed(2)}px 78px rgba(26, 26, 26, ${(0.12 + depth * 0.12).toFixed(3)}), ${x}px ${y}px 48px rgba(255, 77, 109, ${(depth * 0.16).toFixed(3)})`
  }

  if (profile.kind === 'panel') {
    return `${x}px ${y}px 38px rgba(26, 26, 26, ${darkAlpha}), 0 ${Math.max(0, depth * 24).toFixed(2)}px 68px rgba(255, 77, 109, ${glowAlpha})`
  }

  return `${x}px ${y}px 42px rgba(26, 26, 26, ${darkAlpha}), 0 ${blur}px 76px rgba(255, 77, 109, ${glowAlpha})`
}

const initMotionCards = () => {
  if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) return

  const motionElements = document.querySelectorAll(`${motionSelector}:not([data-motion-ready])`)

  motionElements.forEach((element) => {
    element.dataset.motionReady = 'true'
    element.classList.add('motion-reactive')

    const profile = getMotionProfile(element)
    let frameId = 0
    let isHovering = false

    const motion = { x: 0, y: 0, rotateX: 0, rotateY: 0, scale: 1, depth: 0 }
    const target = { x: 0, y: 0, rotateX: 0, rotateY: 0, scale: 1, depth: 0 }
    const lerp = (start, end, amount) => start + (end - start) * amount

    const applyMotion = () => {
      motion.x = lerp(motion.x, target.x, profile.follow)
      motion.y = lerp(motion.y, target.y, profile.follow)
      motion.rotateX = lerp(motion.rotateX, target.rotateX, profile.follow)
      motion.rotateY = lerp(motion.rotateY, target.rotateY, profile.follow)
      motion.scale = lerp(motion.scale, target.scale, profile.follow)
      motion.depth = lerp(motion.depth, target.depth, profile.follow)

      element.style.transform = `perspective(1100px) translate3d(${motion.x}px, ${motion.y}px, 0) rotateX(${motion.rotateX}deg) rotateY(${motion.rotateY}deg) scale(${motion.scale})`
      element.style.setProperty('--motion-depth', motion.depth.toFixed(3))
      element.style.setProperty('--motion-shadow-x', `${motion.x * -0.8}px`)
      element.style.setProperty('--motion-shadow-y', `${Math.max(0, motion.depth * 18)}px`)
      element.style.setProperty('--motion-blur-y', `${Math.max(0, motion.depth * 24)}px`)
      element.style.setProperty('--motion-hard-alpha', (motion.depth * 0.22).toFixed(3))
      element.style.setProperty('--motion-glow-alpha', (motion.depth * 0.16).toFixed(3))
      element.style.setProperty('box-shadow', getMotionShadow(profile, motion), 'important')

      const settled =
        Math.abs(motion.x - target.x) < 0.02 &&
        Math.abs(motion.y - target.y) < 0.02 &&
        Math.abs(motion.rotateX - target.rotateX) < 0.02 &&
        Math.abs(motion.rotateY - target.rotateY) < 0.02 &&
        Math.abs(motion.scale - target.scale) < 0.001 &&
        Math.abs(motion.depth - target.depth) < 0.002

      if (isHovering || !settled) {
        frameId = requestAnimationFrame(applyMotion)
      } else {
        frameId = 0
        element.style.transform = ''
        element.style.removeProperty('--motion-depth')
        element.style.removeProperty('--motion-shadow-x')
        element.style.removeProperty('--motion-shadow-y')
        element.style.removeProperty('--motion-blur-y')
        element.style.removeProperty('--motion-hard-alpha')
        element.style.removeProperty('--motion-glow-alpha')
        element.style.removeProperty('box-shadow')
      }
    }

    const startMotion = () => {
      if (!frameId) {
        frameId = requestAnimationFrame(applyMotion)
      }
    }

    element.addEventListener('pointerenter', () => {
      isHovering = true
      target.y = profile.lift
      target.scale = profile.scale
      target.depth = profile.depth
      startMotion()
    })

    element.addEventListener('pointermove', (event) => {
      const rect = element.getBoundingClientRect()
      const pointerX = ((event.clientX - rect.left) / rect.width - 0.5) * 2
      const pointerY = ((event.clientY - rect.top) / rect.height - 0.5) * 2

      target.x = pointerX * profile.drift
      target.y = profile.lift + pointerY * (profile.drift * 0.6)
      target.rotateX = pointerY * -profile.tilt
      target.rotateY = pointerX * profile.tilt
      target.scale = profile.scale
      target.depth = profile.depth
      startMotion()
    })

    element.addEventListener('pointerleave', () => {
      isHovering = false
      target.x = 0
      target.y = 0
      target.rotateX = 0
      target.rotateY = 0
      target.scale = 1
      target.depth = 0
      startMotion()
    })
  })
}

onMounted(() => {
  initRevealAnimations()
  initMotionCards()
})

watch(() => route.path, () => {
  setTimeout(() => {
    initRevealAnimations()
    initMotionCards()
  }, 50)
})
</script>

<template>
  <AppHeader />

  <div class="site-shell">
    <aside class="brand-rail" aria-hidden="true">NewsCity</aside>

    <div class="site-content">
      <RouterView />
    </div>

    <CommentsPanel />
  </div>

  <AppFooter />
</template>
