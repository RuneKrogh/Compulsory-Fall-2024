import { atom } from 'jotai';
import {Paper} from '../components/main/Api.ts';

export const papersAtom = atom<Paper[]>([]);
