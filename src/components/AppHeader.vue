<template>
  <header>
    <div class="header-container">
      <div class="logo">
        <img src="../assets/nba-logo.svg" alt="" />
        <h1>NBA GameTime</h1>
      </div>
      <nav>
        <router-link to="/">Games</router-link>
        <router-link to="/standings">Standings</router-link>
        <router-link to="/stats">Stats</router-link>
      </nav>
      <nav class="mobile-menu">
        <div class="menu-icon" :class="{ 'cross-icon': showMobileMenu }" @click="toggleMobileMenu">
          <div class="bar"></div>
          <div class="bar"></div>
          <div class="bar"></div>
        </div>
        <div class="mobile-menu-content" v-if="showMobileMenu">
          <router-link to="/">Games</router-link>
          <router-link to="/standings">Standings</router-link>
          <router-link to="/stats">Stats</router-link>
        </div>
      </nav>
    </div>
  </header>
</template>

<script lang="ts">
export default {
  data() {
    return {
      showMobileMenu: false
    }
  },
  methods: {
    toggleMobileMenu() {
      this.showMobileMenu = !this.showMobileMenu

      if (this.showMobileMenu) {
        document.body.classList.add('mobile-menu-open')
      } else {
        document.body.classList.remove('mobile-menu-open')
      }
    }
  }
}
</script>

<style>
header {
  display: flex;
  align-items: center;
  padding: 1rem;
  background-color: rgba(0, 0, 0, 1);
  color: #fff;
  margin-bottom: 2rem;
  position: sticky;
  top: 0;
  z-index: 9999;
}

.header-container {
  display: flex;
  align-items: center;
  max-width: 1280px;
  width: 100%;
  margin: 0 auto;
}

header .logo {
  display: flex;
  align-items: center;
}

header h1 {
  font-size: 1.9rem;
}

header nav {
  display: flex;
  margin-left: 15rem;
  font-size: 1.5rem;
  gap: 3rem;
}

header img {
  width: 3rem;
  margin-right: 1rem;
}

.mobile-menu {
  display: none;
}

.menu-icon {
  cursor: pointer;
  padding: 10px;
  position: relative;
  z-index: 2;
  transition: transform 2s ease;
}

.menu-icon .bar {
  width: 30px;
  height: 3px;
  background-color: #fff;
  margin: 6px 0;
  transition: transform 0.3s ease;
}

body.mobile-menu-open {
  overflow: hidden;
}

.mobile-menu-content {
  animation: slideDown 0.6s ease-in-out;
  background-color: rgba(0, 0, 0, 0.9);
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
  color: #fff;
  display: flex;
  flex-direction: column;
  font-size: 3rem;
  height: 100%;
  justify-content: center;
  left: 0;
  margin: 0;
  padding: 20px;
  position: fixed;
  top: 0;
  width: 100%;
  z-index: 1;
  text-align: center;
}
.cross-icon .bar:nth-child(1) {
  transform: translateY(10px) rotate(45deg);
}

.cross-icon .bar:nth-child(2) {
  opacity: 0;
}

.cross-icon .bar:nth-child(3) {
  transform: translateY(-8px) rotate(-45deg);
}

@media screen and (max-width: 1024px) {
  header nav {
    display: none;
  }

  .mobile-menu {
    display: flex;
    align-items: center;
    margin-left: auto;
  }
}

@media screen and (min-width: 1025px) {
  header nav {
    display: flex;
    margin-left: 15rem;
    font-size: 1.5rem;
    gap: 3rem;
  }
}
@keyframes slideDown {
  0% {
    transform: translateY(-100%);
    opacity: 0;
  }
  100% {
    transform: translateY(0);
    opacity: 1;
  }
}
</style>
