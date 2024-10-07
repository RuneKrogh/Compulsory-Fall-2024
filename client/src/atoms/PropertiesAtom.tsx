import {atom} from 'jotai';
import {Property} from '../components/main/Api.ts';

export const propertiesAtom = atom<Property[]>([]);
