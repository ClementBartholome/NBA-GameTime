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
    </div>
  </header>
</template>

<style>
header {
  display: flex;
  align-items: center;
  padding: 1rem;
  background-color: rgba(0, 0, 0, 0.171);
  color: #fff;
  margin-bottom: 2rem;
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
</style>
