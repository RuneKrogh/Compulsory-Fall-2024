import {atom} from 'jotai';
import {Order} from '../components/main/Api.ts';

export const ordersAtom = atom<Order[]>([]);
