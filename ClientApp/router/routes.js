import Login from 'components/login'
import Upcoming from 'components/upcoming'
import MyRoutes from 'components/my-routes'

export const routes = [
  { name: 'login', path: '/login', component: Login, display: 'Login', icon: 'user' },
  { name: 'upcoming', path: '/upcoming', component: Upcoming, display: 'Upcoming Trails', icon: 'map-signs' },
  { name: 'my-routes', path: '/my-routes', component: MyRoutes, display: 'my-routes', icon: 'user' },
  { path: '*', redirect: '/upcoming' }
]