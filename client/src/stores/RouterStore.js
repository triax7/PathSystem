import {createBrowserHistory} from 'history'
import {RouterStore, syncHistoryWithStore} from 'mobx-react-router'

const routerStore = new RouterStore()
const history = syncHistoryWithStore(createBrowserHistory(), routerStore)

export {routerStore, history}
