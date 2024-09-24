import { atom } from 'jotai';
import { Customer } from '../Api.ts';

export const customersAtom = atom<Customer[]>([]);
