import Router from './router'
import React, { useEffect } from 'react'
import { current } from './apis/Owners'
import { history } from './stores/RouterStore'
import globalStore from './stores/GlobalStore'

async function loadUser() {
  let user = await current()
  if (!user) {
    history.push('/login')
  } else {
    globalStore.user = user
    history.push('/routes')
  }
  console.log(user)
}

function App() {
  useEffect(() => {
    loadUser()
  });

  return (
      <Router/>
  );
}

export default App;
