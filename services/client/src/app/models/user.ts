import {Share} from './share';

export class User {
  id: number;
  name: string;
  password: string;
  shares: Array<Share>;
}
