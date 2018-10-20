import CounterExample from 'components/counter-example'
import Upcoming from 'components/upcoming'
import HomePage from 'components/home-page'
import Carousel from 'components/carousel'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'upcoming', path: '/upcoming', component: Upcoming, display: 'Upcoming Trails', icon: 'map-signs' },
]
