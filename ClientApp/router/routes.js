import CounterExample from 'components/counter-example'
import Upcoming from 'components/upcoming'
import HomePage from 'components/home-page'
import Carousel from 'components/carousel'
import Login from 'components/login'
import Strava from 'components/strava'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'login', path: '/login', component: Login, display: 'Login', icon: 'user' },
  { name: 'upcoming', path: '/upcoming', component: Upcoming, display: 'Upcoming Trails', icon: 'map-signs' },
  { name: 'Profile', path: '/strava', component: Strava, display: 'Profile', icon: 'list' },
  //{ name: 'stravaCallback', path: '/strava/stravaCallback', component: Strava},
  { name: 'counter-example', path: '/counter-example', component: CounterExample, display: 'counter-example', icon: 'fire'}
]
