import { atom } from 'jotai';
import { Customer } from '../components/main/Api.ts';

export const customersAtom = atom<Customer[]>([]);
