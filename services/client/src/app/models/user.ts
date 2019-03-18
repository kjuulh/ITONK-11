import {Shares} from './shares';

export class User {
  userId: number;
  userName: string;
  password: string;
  shares: Array<Shares>;
}
