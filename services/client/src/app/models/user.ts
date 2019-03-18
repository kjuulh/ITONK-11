import {Shares} from './shares';

export class User {
  id: number;
  name: string;
  password: string;
  shares: Array<Shares>;
}
