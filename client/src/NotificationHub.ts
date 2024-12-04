import { HubConnection, HubConnectionBuilder, type IHttpConnectionOptions } from '@aspnet/signalr'
import { useAuthStore } from '@/stores/authStore.ts'

class NotificationHub {
  public client: HubConnection
  private authStore = useAuthStore()

  private connectionOptions: IHttpConnectionOptions = {
    accessTokenFactory: () => {
      if (this.authStore.token) {
        return this.authStore.token;
      } else {
        console.warn('No token available, returning empty string');
        return ''; // Fallback behavior, consider redirecting to login if token is mandatory
      }
    },
  };

  constructor() {
    this.client = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/notifications',{
        accessTokenFactory: this.connectionOptions.accessTokenFactory,
      })
      .build();
  }

  start(){
    this.client.start()
  }
}

export default new NotificationHub();
