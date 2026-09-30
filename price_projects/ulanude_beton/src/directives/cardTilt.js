const motionState = new WeakMap();

const lerp = (start, end, amount) => start + (end - start) * amount;

function animate(element) {
  const state = motionState.get(element);

  if (!state) return;

  state.motion.x = lerp(state.motion.x, state.target.x, 0.1);
  state.motion.y = lerp(state.motion.y, state.target.y, 0.1);
  state.motion.rotateX = lerp(state.motion.rotateX, state.target.rotateX, 0.1);
  state.motion.rotateY = lerp(state.motion.rotateY, state.target.rotateY, 0.1);
  state.motion.scale = lerp(state.motion.scale, state.target.scale, 0.1);
  state.motion.glowX = lerp(state.motion.glowX, state.target.glowX, 0.1);
  state.motion.glowY = lerp(state.motion.glowY, state.target.glowY, 0.1);
  state.motion.depth = lerp(state.motion.depth, state.target.depth, 0.1);

  element.style.transform = `
    perspective(1100px)
    translate3d(${state.motion.x}px, ${state.motion.y}px, 0)
    rotateX(${state.motion.rotateX}deg)
    rotateY(${state.motion.rotateY}deg)
    scale(${state.motion.scale})
  `;
  element.style.setProperty('--hover-glow-x', `${state.motion.glowX}%`);
  element.style.setProperty('--hover-glow-y', `${state.motion.glowY}%`);
  element.style.setProperty('--hover-depth', state.motion.depth.toFixed(3));

  const settled =
    Math.abs(state.motion.x - state.target.x) < 0.02 &&
    Math.abs(state.motion.y - state.target.y) < 0.02 &&
    Math.abs(state.motion.rotateX - state.target.rotateX) < 0.02 &&
    Math.abs(state.motion.rotateY - state.target.rotateY) < 0.02 &&
    Math.abs(state.motion.scale - state.target.scale) < 0.001 &&
    Math.abs(state.motion.depth - state.target.depth) < 0.002;

  if (state.isHovering || !settled) {
    state.frameId = requestAnimationFrame(() => animate(element));
  } else {
    state.frameId = 0;
    element.style.transform = '';
    element.style.removeProperty('--hover-glow-x');
    element.style.removeProperty('--hover-glow-y');
    element.style.removeProperty('--hover-depth');
  }
}

function startAnimation(element) {
  const state = motionState.get(element);

  if (state && !state.frameId) {
    state.frameId = requestAnimationFrame(() => animate(element));
  }
}

export const cardTilt = {
  mounted(element) {
    const state = {
      frameId: 0,
      isHovering: false,
      motion: {
        x: 0,
        y: 0,
        rotateX: 0,
        rotateY: 0,
        scale: 1,
        glowX: 50,
        glowY: 50,
        depth: 0,
      },
      target: {
        x: 0,
        y: 0,
        rotateX: 0,
        rotateY: 0,
        scale: 1,
        glowX: 50,
        glowY: 50,
        depth: 0,
      },
    };

    state.handlePointerEnter = () => {
      state.isHovering = true;
      state.target.y = -8;
      state.target.scale = 1.018;
      state.target.depth = 1;
      startAnimation(element);
    };

    state.handlePointerMove = (event) => {
      const rect = element.getBoundingClientRect();
      const pointerX = ((event.clientX - rect.left) / rect.width - 0.5) * 2;
      const pointerY = ((event.clientY - rect.top) / rect.height - 0.5) * 2;

      state.target.x = pointerX * 7;
      state.target.y = -8 + pointerY * 4;
      state.target.rotateX = pointerY * -4;
      state.target.rotateY = pointerX * 5;
      state.target.scale = 1.018;
      state.target.glowX = 50 + pointerX * 26;
      state.target.glowY = 50 + pointerY * 22;
      state.target.depth = 1;
      startAnimation(element);
    };

    state.handlePointerLeave = () => {
      state.isHovering = false;
      state.target.x = 0;
      state.target.y = 0;
      state.target.rotateX = 0;
      state.target.rotateY = 0;
      state.target.scale = 1;
      state.target.glowX = 50;
      state.target.glowY = 50;
      state.target.depth = 0;
      startAnimation(element);
    };

    element.addEventListener('pointerenter', state.handlePointerEnter);
    element.addEventListener('pointermove', state.handlePointerMove);
    element.addEventListener('pointerleave', state.handlePointerLeave);
    motionState.set(element, state);
  },

  beforeUnmount(element) {
    const state = motionState.get(element);

    if (!state) return;

    element.removeEventListener('pointerenter', state.handlePointerEnter);
    element.removeEventListener('pointermove', state.handlePointerMove);
    element.removeEventListener('pointerleave', state.handlePointerLeave);

    if (state.frameId) {
      cancelAnimationFrame(state.frameId);
    }

    motionState.delete(element);
  },
};
