<script setup>
import { onBeforeUnmount, ref } from 'vue'

defineProps({
  author: {
    type: Object,
    required: true,
  },
  headingTag: {
    type: String,
    default: 'h3',
  },
  showRole: {
    type: Boolean,
    default: false,
  },
})

const cardSurface = ref(null)
let frameId = 0
let isHovering = false

const motion = {
  x: 0,
  y: 0,
  rotateX: 0,
  rotateY: 0,
  scale: 1,
  glowX: 50,
  glowY: 50,
  depth: 0,
}

const target = {
  x: 0,
  y: 0,
  rotateX: 0,
  rotateY: 0,
  scale: 1,
  glowX: 50,
  glowY: 50,
  depth: 0,
}

const lerp = (start, end, amount) => start + (end - start) * amount

const updateSurface = () => {
  const surface = cardSurface.value

  if (!surface) return

  motion.x = lerp(motion.x, target.x, 0.08)
  motion.y = lerp(motion.y, target.y, 0.08)
  motion.rotateX = lerp(motion.rotateX, target.rotateX, 0.08)
  motion.rotateY = lerp(motion.rotateY, target.rotateY, 0.08)
  motion.scale = lerp(motion.scale, target.scale, 0.08)
  motion.glowX = lerp(motion.glowX, target.glowX, 0.08)
  motion.glowY = lerp(motion.glowY, target.glowY, 0.08)
  motion.depth = lerp(motion.depth, target.depth, 0.08)

  surface.style.transform = `perspective(1200px) translate3d(${motion.x}px, ${motion.y}px, 0) rotateX(${motion.rotateX}deg) rotateY(${motion.rotateY}deg) scale(${motion.scale})`
  surface.style.setProperty('--card-glow-x', `${motion.glowX}%`)
  surface.style.setProperty('--card-glow-y', `${motion.glowY}%`)
  surface.style.setProperty('--card-depth', motion.depth.toFixed(3))
  surface.style.setProperty('--card-shadow-x', `${motion.x * -0.9}px`)
  surface.style.setProperty('--card-shadow-y', `${Math.max(10, motion.depth * 36)}px`)

  const settled =
    Math.abs(motion.x - target.x) < 0.02 &&
    Math.abs(motion.y - target.y) < 0.02 &&
    Math.abs(motion.rotateX - target.rotateX) < 0.02 &&
    Math.abs(motion.rotateY - target.rotateY) < 0.02 &&
    Math.abs(motion.scale - target.scale) < 0.001 &&
    Math.abs(motion.depth - target.depth) < 0.002

  if (isHovering || !settled) {
    frameId = requestAnimationFrame(updateSurface)
  } else {
    frameId = 0
    surface.style.transform = ''
    surface.style.removeProperty('--card-glow-x')
    surface.style.removeProperty('--card-glow-y')
    surface.style.removeProperty('--card-depth')
    surface.style.removeProperty('--card-shadow-x')
    surface.style.removeProperty('--card-shadow-y')
  }
}

const startMotion = () => {
  if (!frameId) {
    frameId = requestAnimationFrame(updateSurface)
  }
}

const handlePointerEnter = () => {
  isHovering = true
  target.depth = 1
  target.scale = 1.045
  target.y = -18
  startMotion()
}

const handlePointerMove = (event) => {
  const surface = cardSurface.value

  if (!surface) return

  const rect = surface.getBoundingClientRect()
  const pointerX = ((event.clientX - rect.left) / rect.width - 0.5) * 2
  const pointerY = ((event.clientY - rect.top) / rect.height - 0.5) * 2

  target.x = pointerX * 10
  target.y = -18 + pointerY * 7
  target.rotateX = pointerY * -4
  target.rotateY = pointerX * 5
  target.scale = 1.045
  target.glowX = 50 + pointerX * 26
  target.glowY = 50 + pointerY * 22
  target.depth = 1
  startMotion()
}

const handlePointerLeave = () => {
  isHovering = false
  target.x = 0
  target.y = 0
  target.rotateX = 0
  target.rotateY = 0
  target.scale = 1
  target.glowX = 50
  target.glowY = 50
  target.depth = 0
  startMotion()
}

onBeforeUnmount(() => {
  if (frameId) {
    cancelAnimationFrame(frameId)
  }
})
</script>

<template>
  <div
    class="author-card"
    @pointerenter="handlePointerEnter"
    @pointermove="handlePointerMove"
    @pointerleave="handlePointerLeave"
  >
    <div ref="cardSurface" class="author-card__surface">
      <img :src="author.avatar" :alt="author.name">
      <div>
        <component :is="headingTag">
          <RouterLink :to="`/author/${author.slug}`">
            {{ author.name }}
          </RouterLink>
        </component>
        <p v-if="showRole">{{ author.role }}</p>
        <p>{{ author.bio }}</p>
      </div>
    </div>
  </div>
</template>
