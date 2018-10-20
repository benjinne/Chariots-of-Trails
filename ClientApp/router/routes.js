import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import Carousel from 'components/carousel'
import Login from 'components/login'
import Strava from 'components/strava'

export const routes = [
  { name: 'login', path: '/', component: Login, display: 'Login', icon: 'list' },
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Fetch data', icon: 'list' },
  { name: 'carousel', path: '/carousel', component: Carousel, display: 'Carousel', icon: 'list' },
  { name: 'strava', path: '/strava', component: Strava, display: 'Strava', icon: 'list' }
]
