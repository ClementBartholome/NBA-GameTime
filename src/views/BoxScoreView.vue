<template>
  <AppHeader />
  <main>
    <h1>Box Score for the game {{ gameId }}</h1>
    <BoxScore :playerStats="playerStats" />
  </main>
</template>
